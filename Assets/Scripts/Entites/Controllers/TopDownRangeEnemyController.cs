using System;
using UnityEngine;

public class TopDownRangeEnemyController : TopDownEnemyController
{
    [SerializeField][Range(0f, 100f)] private float followRange = 15f; // 따라다닐 범위 (추적 범위)
    [SerializeField][Range(0f, 100f)] private float ShootRange = 10f; // 공격 범위

    private int layerMaskTarget;

    protected override void Start()
    {
        base.Start();
        layerMaskTarget = stats.CurrentStat.attackSO.target;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        float distanceToTarget = DistanceToTarget();
        Vector2 directionToTarget = DirectionToTarget();

        UpdateEnemyState(distanceToTarget, directionToTarget);  // 에너미 상태 업데이트
    }

    private void UpdateEnemyState(float distanceToTarget, Vector2 directionToTarget)
    {
        IsAttacking = false;
        if (distanceToTarget < followRange) // 지정된 추적 범위 안에 있을 경우
        {
            CheckIfNear(distanceToTarget, directionToTarget); // 공격 범위 내인지 확인
        }
    }

    private void CheckIfNear(float distance, Vector2 direction) // 범위에 따른 공격 여부
    {
        if (distance <= ShootRange)
        {
            TryShootAtTarget(direction); // 사정 거리 안이면 화살 쏨
        }
        else
        {
            CallMoveEvent(direction); // 사정거리 밖이지만 추적 범위 내에 있을 경우, 타겟 쪽으로 이동함
        }
    }

    private void TryShootAtTarget(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, ShootRange, layerMaskTarget); // 오리진(출발), 방향, 범위에 따라 레이캐스트 쏨 
     
        if (hit.collider != null) // 레이캐스트에 맞은 게 있다면
        {
            PerformAttackAction(direction); // 공격함
        }
        else
        {
            CallMoveEvent(direction); // 안맞았으면 이동함
        }
    }

    private void PerformAttackAction(Vector2 direction)
    {
        // 타겟을 정확히 명중했을 경우의 행동 정의
        CallLookEvent(direction);
        CallMoveEvent(Vector2.zero); // 공격 중에는 이동을 멈춤
        IsAttacking = true;
    }
}