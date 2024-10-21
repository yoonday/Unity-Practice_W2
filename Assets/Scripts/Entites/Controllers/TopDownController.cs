using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownController : MonoBehaviour
{
    public event Action<Vector2> OnMoveEvent; // Action�� ������ Void�� ��ȯ�Ѵ�. �ƴϸ� Func ����ؾ� �Ѵ�.
                                              // TopDownMovement�� Start���� Move�� �����ߴ�.
    public event Action<Vector2> OnLookEvent;
    public event Action<AttackSO> OnAttackEvent; // � ������ �ߴ��� �˷���

    protected bool IsAttacking { get; set; } // ��� �޴� Ŭ�������� ���� ����

    private float timeSinceLastAttack = float.MaxValue; // ������ ������ ���� �󸶳� ��������

    // protected ������Ƽ�� �� ���� : ���� �ٲٰ� ������, �������� �� �� ��� �޴� Ŭ�����鵵 �� �� �ֵ���
    protected CharacterStatHandler stats {  get; private set; }

    protected virtual void Awake() // �޼��尡 �θ� Ŭ�������� ���ǵ�����, �ڽ� Ŭ�������� �� �޼��带 ����(������ Override)�� �� �ֵ��� ����Ѵ�.
    {
        stats = GetComponent<CharacterStatHandler>(); // ���� ���� ������Ʈ�� �ִ� CharaterStatHandler ������Ʈ�� ������ Stats ������ �ʱ�ȭ
    }

    private void Update()
    {
        HandleAttackDelay();
    }

    private void HandleAttackDelay()
    {
        if (timeSinceLastAttack < stats.CurrentStat.attackSO.delay) // ��Ÿ�� �ð�
        {
            timeSinceLastAttack += Time.deltaTime; // ��� �ð��� ������Ŵ
        }
        else if(IsAttacking && timeSinceLastAttack >= stats.CurrentStat.attackSO.delay)
        {
            timeSinceLastAttack = 0f;
            CallAttackEvent(stats.CurrentStat.attackSO); // ���� ������ �ִ� ���⿡ ����
        }
    }

    public void CallMoveEvent(Vector2 direction) // ������ �����ϴ� ����
    {
        OnMoveEvent?.Invoke(direction); // ?. ������ ���� ������ �����Ѵ�
                                        // ������ ����Ǵ� �κ�. Event�� �����س��� �޼������ ����ȴ�.
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
