using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownController : MonoBehaviour
{
    public event Action<Vector2> OnMoveEvent; // Action은 무조건 Void만 반환한다. 아니면 Func 사용해야 한다.
                                              // TopDownMovement의 Start에서 Move를 구독했다.
    public event Action<Vector2> OnLookEvent;
    public event Action<AttackSO> OnAttackEvent; // 어떤 공격을 했는지 알려줌

    protected bool IsAttacking { get; set; } // 상속 받는 클래스에서 수정 가능

    private float timeSinceLastAttack = float.MaxValue; // 마지막 공격을 한후 얼마나 지났는지

    // protected 프로퍼티를 한 이유 : 나만 바꾸고 싶지만, 가져가는 건 내 상속 받는 클래스들도 볼 수 있도록
    protected CharacterStatHandler stats {  get; private set; }

    protected virtual void Awake() // 메서드가 부모 클래스에서 정의되지만, 자식 클래스에서 이 메서드를 수정(재정의 Override)할 수 있도록 허용한다.
    {
        stats = GetComponent<CharacterStatHandler>(); // 현재 게임 오브젝트에 있는 CharaterStatHandler 컴포넌트를 가져와 Stats 변수에 초기화
    }

    private void Update()
    {
        HandleAttackDelay();
    }

    private void HandleAttackDelay()
    {
        if (timeSinceLastAttack < stats.CurrentStat.attackSO.delay) // 쿨타임 시간
        {
            timeSinceLastAttack += Time.deltaTime; // 경과 시간을 누적시킴
        }
        else if(IsAttacking && timeSinceLastAttack >= stats.CurrentStat.attackSO.delay)
        {
            timeSinceLastAttack = 0f;
            CallAttackEvent(stats.CurrentStat.attackSO); // 현재 가지고 있는 무기에 따름
        }
    }

    public void CallMoveEvent(Vector2 direction) // 실행을 지시하는 역할
    {
        OnMoveEvent?.Invoke(direction); // ?. 없으면 말고 있으면 실행한다
                                        // 실제로 실행되는 부분. Event에 저장해놓은 메서드들이 실행된다.
    }

    public void CallLookEvent(Vector2 direction)
    {
        OnLookEvent?.Invoke(direction);
    } 
    private void CallAttackEvent(AttackSO attackSO)
    {
        OnAttackEvent?. Invoke(attackSO);
    }
}
