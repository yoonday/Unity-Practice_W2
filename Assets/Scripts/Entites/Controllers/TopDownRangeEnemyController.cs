using System;
using UnityEngine;

public class TopDownRangeEnemyController : TopDownEnemyController
{
    [SerializeField][Range(0f, 100f)] private float followRange = 15f; // ����ٴ� ���� (���� ����)
    [SerializeField][Range(0f, 100f)] private float ShootRange = 10f; // ���� ����

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

        UpdateEnemyState(distanceToTarget, directionToTarget);  // ���ʹ� ���� ������Ʈ
    }

    private void UpdateEnemyState(float distanceToTarget, Vector2 directionToTarget)
    {
        IsAttacking = false;
        if (distanceToTarget < followRange) // ������ ���� ���� �ȿ� ���� ���
        {
            CheckIfNear(distanceToTarget, directionToTarget); // ���� ���� ������ Ȯ��
        }
    }

    private void CheckIfNear(float distance, Vector2 direction) // ������ ���� ���� ����
    {
        if (distance <= ShootRange)
        {
            TryShootAtTarget(direction); // ���� �Ÿ� ���̸� ȭ�� ��
        }
        else
        {
            CallMoveEvent(direction); // �����Ÿ� �������� ���� ���� ���� ���� ���, Ÿ�� ������ �̵���
        }
    }

    private void TryShootAtTarget(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, ShootRange, layerMaskTarget); // ������(���), ����, ������ ���� ����ĳ��Ʈ �� 
     
        if (hit.collider != null) // ����ĳ��Ʈ�� ���� �� �ִٸ�
        {
            PerformAttackAction(direction); // ������
        }
        else
        {
            CallMoveEvent(direction); // �ȸ¾����� �̵���
        }
    }

    private void PerformAttackAction(Vector2 direction)
    {
        // Ÿ���� ��Ȯ�� �������� ����� �ൿ ����
        CallLookEvent(direction);
        CallMoveEvent(Vector2.zero); // ���� �߿��� �̵��� ����
        IsAttacking = true;
    }
}