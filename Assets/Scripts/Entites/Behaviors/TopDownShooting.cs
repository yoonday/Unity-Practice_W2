using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class TopDownShooting : MonoBehaviour // 원거리용
{
    // TopDownController에 함수를 등록하는 과정이 필요함
    private TopDownController controller;

    [SerializeField] private Transform projectileSpawnPosition; // 발사체가 스폰될 위치
    private Vector2 aimDirection = Vector2.right; // 조준 방향


    

    private void Awake()
    {
        controller = GetComponent<TopDownController>();
        
    }

    private void Start()
    {
        controller.OnAttackEvent += OnShoot;
        controller.OnLookEvent += OnAim;
    }

    private void OnAim(Vector2 direction) // OnLookEvent가 Vector2값을 받기 때문에 자동으로 파라미터가 Vector2로 잡힘
    {
        aimDirection = direction; // 마우스가 움직일 때마다 aimDirection이 바뀜
    }

    private void OnShoot(AttackSO attackSO) 
    {   
        RangedAttackSO rangedAttackS0 = attackSO as RangedAttackSO; // attackSO를 RangedAttackSO로 형변환
        if (rangedAttackS0 == null) return; // 형변환이 실패하면(null) 아래 내용 실행 의미 X

        float projectilesAngleSpace = rangedAttackS0.multipleProjectilesAngle; // 발사체 사이 각도
        int numberOfProjectilesPershot = rangedAttackS0.numberOfProjectilePerShot; // 한 번에 몇 발 나가는지

        // 제일 첫 번째 발사체가 나가는 각도
        float minAngle = -(numberOfProjectilesPershot / 2f) * projectilesAngleSpace + 0.5f * rangedAttackS0.multipleProjectilesAngle;
        for (int i = 0; i < numberOfProjectilesPershot; i++) // 첫 번째 발사체 기준으로 각도 더해주기
        {
            float angle = minAngle + i*projectilesAngleSpace; // 발사체 사이 각도씩 추가해줌
            float randomSpread = Random.Range(-rangedAttackS0.spread, rangedAttackS0.spread); // 시스템과 유니티엔진의 Random이 겹처 생기는 문제 해결 : Random = UnityEngine.Random; 선택
                                                                                              // 스프레드 만큼 각도에 영향
            angle += randomSpread;
            CreateProjectile(rangedAttackS0, angle); // 투사체 생성
        }

    }

    private void CreateProjectile(RangedAttackSO rangedAttackSO, float angle) // 실제로 날아가게 만드는 함수
    {
        GameObject obj = GameManager.Instance.ObjectPool.SpawnFromPool(rangedAttackSO.bulletNameTag);
        obj.transform.position = projectileSpawnPosition.position; // 발사체가 생성될 초기 위치 설정
        ProjectileController attackController = obj.GetComponent<ProjectileController>(); // 발사체와 관련된 컴포넌트를 가져와 부착된 스크립트의 기능을 사용한다.
        attackController.InitializeAttack(RotateVector2(aimDirection, angle), rangedAttackSO); // InitializeAttack - 발사체 초기화, RotateVector2 회전된 벡터값과 발사체 정보 전달
    }
    private static Vector2 RotateVector2(Vector2 v, float angle) // 입력받은 벡터 v 일정한 각도 angle만큼 회전시킨 새로운 방향을 반환
    {
        return Quaternion.Euler(0, 0, angle) * v; // 앵글로 설정되어있어, 오일러 각도로 맞춘 후 곱해준다.
                                                  // angle만큼 회전하는 쿼터니언 생성 후 벡터에 곱하여 회전된 벡터 반환
    }
}