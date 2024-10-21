using System;
using UnityEngine;

public class TopDownMovement : MonoBehaviour
{
    // 실제로 이동이 일어날 컴포넌트

    private TopDownController controller;
    private Rigidbody2D movementRigidbody;
    private CharacterStatHandler characterStatHandler; // 추가적인 로직 적용을 위함

    private Vector2 movementDirection = Vector2.zero; // 오류 방지 차원

    //넉백을 위한 변수
    private Vector2 knockback = Vector2.zero;
    private float knockbackDuration = 0.0f;


    private void Awake()
    {
        // Awake : 주로 내 컴포넌트 안에서 끝나는 것. 게임 시작 전 컴포넌트의 세팅 등
        
        // 자주 사용할 컨트롤러 캐싱 작업 
        controller = GetComponent<TopDownController>(); // 이 내용으로 확인할 수 있는 것
                                                        // controller와 TopDownCotroller가 같은 게임 오브젝트 안에 있다고 가정
                                                        // GetCompnent앞에 다른 게임오브젝트가 안붙어있기 때문
        movementRigidbody = GetComponent<Rigidbody2D>();
        characterStatHandler = GetComponent<CharacterStatHandler>(); // currentStat을 가져오게 하기 위해 연결함



    }

    private void Start()
    {
        controller.OnMoveEvent += Move;
    }

    private void Move(Vector2 direction) // Update에서 실행. 프레임 기반
    {
        movementDirection = direction;
    }

    private void FixedUpdate()
    {
        // FixedUpdate : 물리 업데이트 관련. 업데이트 기반
        // rigidbody 값을 바꿈
        // movementDirection의 값을 받아 실제로 처리함
        ApplyMovement(movementDirection);

        if (knockbackDuration > 0.0f)
        {
            knockbackDuration -= Time.fixedDeltaTime; // FixedUpdate에서 실행되기에, fixedDeltaTime을 사용해야 함
                                                      // DeltaTime 쓸 경우 FixedUpdate와 호출 시기와 달라 더 오래돌거나 덜 돌 수 있다.  
        }
    }

    public void ApplyKnockback(Transform Other, float power, float duration) // 다른 곳(Other)에서 보내준 값 적용
    {
        knockbackDuration = duration;
        knockback = -(Other.position - transform.position).normalized * power;
    }

    private void ApplyMovement(Vector2 direction)
    {
        direction = direction * characterStatHandler.CurrentStat.speed; // 현재 스탯(CurrentStat)의 속도를 가져온다 - 버프 등을 통한 현재 능력치 반영을 위함
        
        if (knockbackDuration > 0.0f)
        {
            direction += knockback;
        }

        movementRigidbody.velocity = direction;
    }
}