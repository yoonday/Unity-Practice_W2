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

    private void OnDeath() // ĳ���͸� �������ϰ��ϰ�, ������ �ִ� ������Ʈ�� ���ִ� �۾�
    {
        rigidbody.velocity = Vector2.zero;

        foreach (SpriteRenderer renderer in GetComponentsInChildren<SpriteRenderer>()) // ���ΰ� ���� ������Ʈ���� ������Ʈ �� Sprite Renderer�� ������ �ݺ�
        {
            Color color = renderer.color; // Color ����ü�� �� ������ - �ʵ峪 �Ӽ��� ������ �� ����, �����ؼ� ���� ������ �־�߸� ��
            color.a = 0.3f; // �� ���� 
            renderer.color = color;  // �ٽ� �־� ��
            // rederer.color.a = 0.3f�� �۵� ����
        }

        foreach (Behaviour behaviour in GetComponentsInChildren<Behaviour>()) // Behavior = Ȱ��ȭ/��Ȱ��ȭ ������ ��� ������Ʈ
        {
            behaviour.enabled = false; // ��� ������Ʈ ��Ȱ��ȭ �ݺ�
        }

        Destroy(gameObject, 2f); // 2�� �ڿ� ���� ������Ʈ �ı� - ������ �� ���ϸ�ŭ ����
    }
}