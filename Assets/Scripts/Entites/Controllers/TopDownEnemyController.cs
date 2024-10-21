using UnityEditor.SceneManagement;
using UnityEngine;


public class TopDownEnemyController : TopDownController // TopDownController ���
{
    protected Transform ClosestTarget { get; private set; } // �ٸ� ������ ���� X

    protected override void Awake() 
    {
        base.Awake(); // TopDownController ���ؼ� Stat �ʱ�ȭ ����
    }

    protected virtual void Start() // ��� ���� Ÿ�Կ� ����ǵ���
    {
        ClosestTarget = GameManager.Instance.Player; // ������ Ÿ���� �÷��̾�� ����(�÷��̾ 1���̱⿡ ������ �� ����)
    }

    protected virtual void FixedUpdate()
    {

    }

    protected float DistanceToTarget() // Ÿ�ٰ��� �Ÿ�
    {
        return Vector3.Distance(transform.position, ClosestTarget.position);
    }

    protected Vector2 DirectionToTarget() // ����
    {
        return (ClosestTarget.position - transform.position).normalized;
    }

}
