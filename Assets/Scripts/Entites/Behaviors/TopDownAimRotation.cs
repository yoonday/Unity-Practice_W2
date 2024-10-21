using System;
using UnityEngine;

public class TopDownAimRotation : MonoBehaviour
{
    [SerializeField] private SpriteRenderer armRenderer; // 화살이 뒤집히는 효과
    [SerializeField] private Transform armPivot;

    [SerializeField] private SpriteRenderer characterRenderer; // 에임에 따라 캐릭터가 뒤집히는 효과

    private TopDownController controller;

    private void Awake()
    {
        controller = GetComponent<TopDownController>();
    }

    private void Start()
    {
        // 마우스의 위치가 들어오는 OnLookEvent에 등록함
        // 마우스의 위치를 받아서 팔을 돌리는 데 활용
        controller.OnLookEvent += OnAim; // RotateArm 대신 OnAim을 만들어 사용하지 않는 이유
                                         // 입력처리하는 기능을 분리하고, OnAim을 통해서 다른 액션과 연결할 수 있다.
    }

    private void OnAim(Vector2 direction) 
    {
        RotateArm(direction);
    }

    private void RotateArm(Vector2 direction)
    {
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // 라디안을 Rad2Deg 사용하여 디그리로 변환
                                                                            // 쿼터니안 오일러로 rotation의 z값에 넣어줄 것이기 때문에
        characterRenderer.flipX = Mathf.Abs(rotZ) > 90f; // 캐릭터 뒤집기
                                                         // Abs 절대값을 의미함 - 왼쪽을 바라보고 있을 때 x축 방향을 뒤집는다
        armRenderer.flipY = characterRenderer.flipX; // 캐릭터 렌더러의 flip X 값과 같은 값으로 y축을 플립하여 위 아래를 바꿔줌 (상하대칭이 아니어서)
        armPivot.rotation = Quaternion.Euler(0,0,rotZ); // 팔돌리기 - z축 회전
        // armPivot.eulerAngles = new Vector(0,0,rotZ) 위 rotation과 같은 방식으로 라디안값을 쿼터니언으로 변환한다.
    }
}