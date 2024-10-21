using System;
using UnityEngine;

public class TopDownAnimationController : AnimationController
{
    // 다른 곳에서 수정할 일X. 읽기 전용으로 세팅
    private static readonly int isWalking = Animator.StringToHash("isWalking"); 
    private static readonly int isHit = Animator.StringToHash("isHit");
    private static readonly int Attack = Animator.StringToHash("attack");

    private readonly float magnituteThreshold = 0.5f;  // 움직임이 너무 작으면 멈춤 처리함
                                                       // magnitute(벡터 길이) 기준값으로 0.5 지정

    // 무적 표현을 위해 필요함
    private HealthSystem healthSystem;

    protected override void Awake() // 부모 클래스의 Awake 오버라이드
    {
        base.Awake(); // 부모 클래스의 Awake를 실행한다는 뜻
        healthSystem = GetComponent<HealthSystem>();
    }

    private void Start()
    {
        controller.OnAttackEvent += Attacking; // 이벤트에 애니메이션 추가
        controller.OnMoveEvent += Move;

        if (healthSystem != null)
        {
            healthSystem.OnDamage += Hit; // 함수 등록
            healthSystem.OnInvincibilityEnd += InvincibilityEnd;
        }
    }

    private void Move(Vector2 vector)
    {
        animator.SetBool(isWalking, vector.magnitude > magnituteThreshold); // 움직인 거리 판단해서 실행
    }

    private void Attacking(AttackSO sO)
    {
        animator.SetTrigger(Attack); // 때리면 무조건 실행
    }

    private void Hit() // 피격 관련 - 색 변경
    {
        animator.SetBool(isHit, true);
    }

    private void InvincibilityEnd() // 무적이 되면
    {
        animator.SetBool(isHit, false);
    }
   
}