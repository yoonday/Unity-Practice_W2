using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    // 무적 시간
    [SerializeField] private float healthChangeDelay = 0.5f; // 이 시간이 지난 후 피해 받음

    private CharacterStatHandler statHandler; // 스탠 관리
    private float timeSinceLastChange = float.MaxValue; // 마지막 공격 후 얼마나 시간이 지났는지

    private bool isAttacked = false; // 공격 받았는지
    
    
    // 체력이 변했을 때 어떤 일이 일어나는지
    public event Action OnDamage;
    public event Action OnHeal;
    public event Action OnDeath;
    public event Action OnInvincibilityEnd;

    // 여기서 '=>'는 표현식 본문(식 본문)을 의미함. 속성 값 반환을 위해 접근에 용이하도록 세팅(외부, 너무 긴 접근 경로)
    public float MaxHealth => statHandler.CurrentStat.maxHealth; // Get 접근자를 간결하게 표현

    public float CurrentHealth { get; private set; }

    private void Awake() // 이 스크립트 관련된 것 우선 실행
    {
        statHandler = GetComponent<CharacterStatHandler>(); 
    }

    private void Start()
    {
       CurrentHealth = MaxHealth; // 처음에는 꽉 찬 상태로 시작
    }

    
    private void Update() // 공격을 받았을 때 타이머가 지속되도록 함
    {
        if (isAttacked && timeSinceLastChange < healthChangeDelay) 
        {
            timeSinceLastChange += Time.deltaTime; 
            if (timeSinceLastChange > healthChangeDelay) // 값을 더하다가 무적 시간이 지나면
            {
                OnInvincibilityEnd?.Invoke();
                isAttacked = false;
            }
        }
    }

    public bool ChangeHealth(float change) // 체력을 깎는 함수 - bool인 이유 : 
    {
        if (timeSinceLastChange < healthChangeDelay) // 무적상태
        {
            return false; // 체력이 변동되지 않음
        }

        timeSinceLastChange = 0;
        CurrentHealth += change; // change의 값이 양수냐 음수냐에 따라 체력이 회복하거나 피해받게 됨
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth); // 값의 범위 제한. Mathf.Clamp(제한할 값, 최소, 최대)

        if (CurrentHealth <= 0f) // 체력이 0 이하 = 죽음
        {
            CallDeath();
            return true; // 데미지를 준 건 맞기에 true 반환
        }
        if (change >= 0) // 회복
        {
            OnHeal?.Invoke();
        }
        else // 피해 
        {
            OnDamage?.Invoke();
            isAttacked = true;
        }

        return true; // 체력 변화가 정상적으로 이루어진 것을 의미 (일관된 반환값 유지)
    }

    private void CallDeath()
    {
        OnDeath?.Invoke();  
    }
}
