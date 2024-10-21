using UnityEditor.SceneManagement;
using UnityEngine;


public class TopDownEnemyController : TopDownController // TopDownController 상속
{
    protected Transform ClosestTarget { get; private set; } // 다른 곳에서 세팅 X

    protected override void Awake() 
    {
        base.Awake(); // TopDownController 통해서 Stat 초기화 받음
    }

    protected virtual void Start() // 모든 몬스터 타입에 적용되도록
    {
        ClosestTarget = GameManager.Instance.Player; // 몬스터의 타겟을 플레이어로 설정(플레이어가 1명이기에 적용할 수 있음)
    }

    protected virtual void FixedUpdate()
    {

    }

    protected float DistanceToTarget() // 타겟과의 거리
    {
        return Vector3.Distance(transform.position, ClosestTarget.position);
    }

    protected Vector2 DirectionToTarget() // 방향
    {
        return (ClosestTarget.position - transform.position).normalized;
    }

}
