using System;
using UnityEngine;

public class TopDownContactEnemyController : TopDownEnemyController
{
    [SerializeField][Range(0f, 100f)] private float followRange; // 따라갈 범위
    [SerializeField] private string targetTag = "Player"; // 타겟 태그 플레이어로 설정
    private bool isCollidingWithTarget;

    [SerializeField] private SpriteRenderer characterRenderer;  // 플레이어의 위치에 따라 몹이 바라보는 방향이 바뀌게 할 것

    // HealthSystem 
    HealthSystem healthSystem; // 내 healthSystem
    private HealthSystem collidingTargetHealthSystem; // 단거리 적의 healthSystem - 여기에 충돌한 적이 받은 데미지 값 저장
    private TopDownMovement collidingMovement; // 단거리적도 넉백 효과를 줄 수 있어서
    
    protected override void Start()
    {
        base.Start();

        healthSystem = GetComponent<HealthSystem>();
        healthSystem.OnDamage += OnDamage;
    }

    private void OnDamage()
    {
        followRange = 100f; // 무조건 따라가게 하는 역할
    }

    protected override void FixedUpdate() // 물리적인 연산
    {
        base.FixedUpdate();

        if (isCollidingWithTarget)  // 타겟과 충돌했다면
        {
            ApplyHealthChange(); // 체력 변화를 줘라
        }


        Vector2 direction = Vector2.zero; // zero (0, 0). 속도, 방향이 없다는 뜻 - 정지상태를 의미함
        if (DistanceToTarget() < followRange) // 적과의 거리가 따라갈 범위 이내인지 확인
        {
            direction = DirectionToTarget();
        }

        CallMoveEvent(direction);
        Rotate(direction); //
    }

    private void ApplyHealthChange() // 데미지 주는 방법
    {
        AttackSO attackSO = stats.CurrentStat.attackSO;
        bool isAttackable = collidingTargetHealthSystem.ChangeHealth(-attackSO.power); // 공격 가능한 상태 때 피해를 입힘 (무적상태일 때는 데미지를 줄 수 없음)
    
        if (isAttackable && attackSO.isOnKnockBack && collidingMovement != null) // 공격 가능하고, 넉백이 되어있으면
        {
            collidingMovement.ApplyKnockback(transform, attackSO.knockbackPower, attackSO.knockbackPower); // 방향에 넉백의 효과 적용
        }
    }

    private void Rotate(Vector2 direction) // using UnityEngine.UIElements;안 함수와 이름이 겹쳐서 상단 Using문 삭제 해야함
    {
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        characterRenderer.flipX = Mathf.Abs(rotZ) > 90f; // 절대값 90기준으로 방향 전환
    }

    private void OnTriggerEnter2D(Collider2D collision) // 닿았을 때의 값들
    {
        GameObject receiver = collision.gameObject; // 충돌을 받은 애

        if (!receiver.CompareTag(targetTag)) // 리시버가 'targetTag'가 아니라면
        {
            return; // 무시함 - 얼리 리턴. if문을 타고 가서 종료하는 것보다 훨씬 좋음
        }

        collidingTargetHealthSystem = collision.GetComponent<HealthSystem>();
        if (collidingTargetHealthSystem != null)
        {
            isCollidingWithTarget = true; // 닿고있다 = true
        }

        collidingMovement = collision.GetComponent<TopDownMovement>(); // 닿은 것들의 값을 얻을 수 있음
    }

    private void OnTriggerExit2D(Collider2D collision) // 닿아있는 상태가 아닐 때
    {
        if (!collision.CompareTag(targetTag)) { return; } // 타겟 태그가 맞지 않다면 return
        isCollidingWithTarget = false; // 더 이상 데미지를 주면 안된다는 알림
    }

}