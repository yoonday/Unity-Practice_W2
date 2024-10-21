using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    // ���� �ð�
    [SerializeField] private float healthChangeDelay = 0.5f; // �� �ð��� ���� �� ���� ����

    private CharacterStatHandler statHandler; // ���� ����
    private float timeSinceLastChange = float.MaxValue; // ������ ���� �� �󸶳� �ð��� ��������

    private bool isAttacked = false; // ���� �޾Ҵ���
    
    
    // ü���� ������ �� � ���� �Ͼ����
    public event Action OnDamage;
    public event Action OnHeal;
    public event Action OnDeath;
    public event Action OnInvincibilityEnd;

    // ���⼭ '=>'�� ǥ���� ����(�� ����)�� �ǹ���. �Ӽ� �� ��ȯ�� ���� ���ٿ� �����ϵ��� ����(�ܺ�, �ʹ� �� ���� ���)
    public float MaxHealth => statHandler.CurrentStat.maxHealth; // Get �����ڸ� �����ϰ� ǥ��

    public float CurrentHealth { get; private set; }

    private void Awake() // �� ��ũ��Ʈ ���õ� �� �켱 ����
    {
        statHandler = GetComponent<CharacterStatHandler>(); 
    }

    private void Start()
    {
       CurrentHealth = MaxHealth; // ó������ �� �� ���·� ����
    }

    
    private void Update() // ������ �޾��� �� Ÿ�̸Ӱ� ���ӵǵ��� ��
    {
        if (isAttacked && timeSinceLastChange < healthChangeDelay) 
        {
            timeSinceLastChange += Time.deltaTime; 
            if (timeSinceLastChange > healthChangeDelay) // ���� ���ϴٰ� ���� �ð��� ������
            {
                OnInvincibilityEnd?.Invoke();
                isAttacked = false;
            }
        }
    }

    public bool ChangeHealth(float change) // ü���� ��� �Լ� - bool�� ���� : 
    {
        if (timeSinceLastChange < healthChangeDelay) // ��������
        {
            return false; // ü���� �������� ����
        }

        timeSinceLastChange = 0;
        CurrentHealth += change; // change�� ���� ����� �����Ŀ� ���� ü���� ȸ���ϰų� ���عް� ��
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth); // ���� ���� ����. Mathf.Clamp(������ ��, �ּ�, �ִ�)

        if (CurrentHealth <= 0f) // ü���� 0 ���� = ����
        {
            CallDeath();
            return true; // �������� �� �� �±⿡ true ��ȯ
        }
        if (change >= 0) // ȸ��
        {
            OnHeal?.Invoke();
        }
        else // ���� 
        {
            OnDamage?.Invoke();
            isAttacked = true;
        }

        return true; // ü�� ��ȭ�� ���������� �̷���� ���� �ǹ� (�ϰ��� ��ȯ�� ����)
    }

    private void CallDeath()
    {
        OnDeath?.Invoke();  
    }
}
