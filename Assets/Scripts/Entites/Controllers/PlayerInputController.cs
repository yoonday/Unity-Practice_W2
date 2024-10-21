using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : TopDownController
{
    private Camera camera;
    protected override void Awake()
    {
        base.Awake(); // 부모 클래스의 Awake를 실행시킨다 - 부모 클래스에서 실행하는 부분이 빠지게 되면 stat을 가져오지 못한다.
        camera = Camera.main; // mainCamera라는 태그가 붙어있는 카메라를 가져온다. (오버라이딩한 내역)
    }

    public void OnMove(InputValue value) // 입력받아서 전달하는 역할
    {
        Vector2 moveInput = value.Get<Vector2>().normalized; // 정규화. 크기를 1인 벡터로 만듦
        CallMoveEvent(moveInput);  // TopDownController를 상속받았기 때문에 사용할 수 있음
                                   // 입력받은 걸 가지고 실제로 실행하도록 지시함
        
    }

    public void OnLook(InputValue value)
    {
        Vector2 newAim = value.Get<Vector2>(); // 마우스 위치에서는 normalized를 하지 않아도 된다. (벡터의 크기나 방향을 바로 사용할 수 있기 때문)
        Vector2 worldPos = camera.ScreenToWorldPoint(newAim); // 카메라의 범위 : 카메라를 기준으로 마우스가 찍은 위치(스크린 좌표계)에서 월드 좌표계로 바꿔라.
        newAim = (worldPos - (Vector2)transform.position).normalized; // 캐릭터의 방향을 결정하기 위함
                                                                      // 캐릭터의 현재 위치에서 마우스가 클릭된 월드 좌표까지의 방향 계산
                                                                      // 어느 방향으로 캐릭터가 바라봐야하는지를 의미함

        CallLookEvent(newAim); // Look 이벤트 발생
    }

    public void OnFire(InputValue value) // Input Action에 등록한 것 기반
    {
        IsAttacking = value.isPressed; // IsAttacking은 TopDownController에 정의
    }
}