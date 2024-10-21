using System;
using UnityEngine;

public class DestroyOnDeath : MonoBehaviour
{
    private HealthSystem healthSystem;
    private Rigidbody2D rigidbody;

    private void Start()
    {
        healthSystem = GetComponent<HealthSystem>();
        rigidbody = GetComponent<Rigidbody2D>();
        healthSystem.OnDeath += OnDeath;
    }

    private void OnDeath() // 캐릭터를 반투명하게하고, 가지고 있는 컴포넌트를 꺼주는 작업
    {
        rigidbody.velocity = Vector2.zero;

        foreach (SpriteRenderer renderer in GetComponentsInChildren<SpriteRenderer>()) // 본인과 하위 오브젝트들의 컴포넌트 중 Sprite Renderer를 가져와 반복
        {
            Color color = renderer.color; // Color 구조체는 값 형식임 - 필드나 속성에 접근할 수 없고, 복사해서 값을 설정해 주어야만 함
            color.a = 0.3f; // 값 변경 
            renderer.color = color;  // 다시 넣어 줌
            // rederer.color.a = 0.3f는 작동 안함
        }

        foreach (Behaviour behaviour in GetComponentsInChildren<Behaviour>()) // Behavior = 활성화/비활성화 가능한 모든 컴포넌트
        {
            behaviour.enabled = false; // 모든 컴포넌트 비활성화 반복
        }

        Destroy(gameObject, 2f); // 2초 뒤에 게임 오브젝트 파괴 - 투명한 거 보일만큼 여유
    }
}