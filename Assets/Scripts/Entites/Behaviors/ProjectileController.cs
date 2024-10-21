using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ProjectileController : MonoBehaviour // 투사체(Arrow) 컨트롤러
{
    [SerializeField] private LayerMask levelCollisionLayer; // 레벨이랑 닿았는지 검사

    private bool isReady;

    private Rigidbody2D rigidbody;
    private SpriteRenderer spriteRenderer;
    private TrailRenderer trailRenderer;

    private RangedAttackSO attackData; // InitializeAttack에서 사용하기 위해 가져옴
    private float currentDuration;
    private Vector2 direction;
    private bool fxOnDestroy = true; // 기본적으로 보이도록

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>(); // 'Arrow' 게임 오브젝트의 자식 오브젝트에 달려있는 스프라이트 렌더러 가져옴
        rigidbody = GetComponent<Rigidbody2D>();
        trailRenderer = GetComponent<TrailRenderer>();
    }

    private void Update()
    {
        if (!isReady) // 
        {
            return;
        }

        currentDuration += Time.deltaTime;

        if (currentDuration > attackData.duration) // 화살을 쏜지 오래면
        {
            DestroyProjectile(transform.position, false); // 투사체가 사라지는 함수
        }

        rigidbody.velocity = direction * attackData.speed;
    }


    public void InitializeAttack(Vector2 direction, RangedAttackSO attackData)
    {
        this.attackData = attackData;
        this.direction = direction;

        UpdateProjectileSprite(); // 스프라이트 이미지 변경
        trailRenderer.Clear();
        currentDuration = 0;
        spriteRenderer.color = attackData.projectileColor;

        transform.right = this.direction;

        isReady = true;
    }


    private void DestroyProjectile(Vector3 position, bool createFx)
    {
       if (createFx) // 시각 효과의 포함 유무
        {
            // TODO :: ParticleSystem에 대해 배우고, 무기 NameTag로 해당하는 FX 가져오기
        }
       gameObject.SetActive(false);
    }
    private void UpdateProjectileSprite() // 스프라이트 크기 변경 - 모든 방향으로 균일하게 크기가 조정된다. 
    {
        transform.localScale = Vector3.one * attackData.size;
        // 게임 오브젝트의 크기 = Vector3(1,1,1) * 발사체의 사이즈 
        //  Vector3.one을 곱하는 것 → x, y, z에 대해 같은 비율의 사이즈가 적용된다.
    }


    private void OnTriggerEnter2D(Collider2D collision) // 트리거(발사체)가 다른 오브젝트와 충돌했을 때 (매개변수는 발사체가 충돌한 상대 오브젝트)
    {
        if (isLayerMatched(levelCollisionLayer.value, collision.gameObject.layer)) // 충돌해야하는 특정 레이어와 충돌한 타겟 게임 오브젝트의 레이어 일치 여부 확인
        {
            Vector2 destroyPosition = collision.ClosestPoint(transform.position) - direction * 0.2f; // transform.position 스크립트가 적용된 게임 오브젝트(발사체)의 현재 위치
                                                                                                     // 충돌 오브젝트에서 발사체의 위치에 가장 가까운 충돌 지점 반환
                                                                                                     // 뒤에 - direction *0.2f로 살짝 뒤로 움직이게 해 충돌 지점 자연스럽게 꾸밈
            DestroyProjectile(destroyPosition, fxOnDestroy); // 충돌 지점에서 파괴, 파괴 효과 적용
        }
        else if (isLayerMatched(attackData.target.value, collision.gameObject.layer)) // 원하는 타겟에 맞췄을 때
        {
            HealthSystem healthSystem = collision.GetComponent<HealthSystem>(); // 충돌한 오브젝트에서 HealthSystem 컴포넌트 가져옴
            if (healthSystem != null) // healthSystem이 없을 가능성은 거의 없지만 예외처리를 위함
            {
                bool isAttackApplied = healthSystem.ChangeHealth(-attackData.power); // 피해를 줌

                if (isAttackApplied && attackData.isOnKnockBack) // 피해를 주고 넉백인 무기이면
                {
                    ApplyKnockback(collision); // 넉백을 줌
                }
            }
            DestroyProjectile(collision.ClosestPoint(transform.position), fxOnDestroy); // 충돌 지점에서 파괴
        }

    }

    private void ApplyKnockback(Collider2D collision) // 넉백 적용
    {
        TopDownMovement movement = collision.GetComponent<TopDownMovement>();
        if (movement != null)
        {
            movement.ApplyKnockback(transform, attackData.knockbackPower, attackData.knockbackTime); // 내 위치를 기준으로 방향 계산, 힘, 지속 시간
        }
    }

    private bool isLayerMatched(int value, int layer) // value 레이어 마스크(상자). layer 레이어 인덱스 값 비교
    {
        return value == (value | 1 << layer); // 비트 연산 사용. 특정 레이어에 value의 비트값이 포함되어 있는지 확인
                                              // 1 << layer : 숫자 1을 layer만큼 왼쪽으로 이동시킴
                                              // 비트 OR 연산 : Value와 1 << layer 비교 - 둘 중 하나라도 1이라면 비트의 결과가 1이 됨 
                                              // value에 layer가 추가됨 - 비트가 켜지면 비트값이 포함되어 있지 않았다는 뜻이 됨. 그럼 초기에 value값과 달라지게 됨
                                              // value == (value | 1 << layer) : 비트 레이어가 켜져있었다면 true
                                              // 두 매개변수의 정수값을 비교하는 것이 아님. 비트 값의 상태를 비교
    }
}
