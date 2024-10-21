// 능력치가 변하는 것까지 저장함

using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatHandler : MonoBehaviour // Unity의 컴포넌트 시스템을 사용하기 위해서 MonoBehaviour을 상속 받는다
{
    // 기본 스탯과 추가 스탯을 계산해서 최종 스탯을 계산하는 로직
    // 지금은 기본 스탯만 구현

    [SerializeField] private CharacterStat baseStat; // 캐릭터 스탯 저장할 변수
    public CharacterStat CurrentStat { get; private set; } // 현재 능력치 저장 → 추후 최종 스탯이 됨
    
    public List<CharacterStat> statModifiers = new List<CharacterStat>(); // 어떤 추가 스탯(버프)이 있는지 담아놓는 리스트 생성
                                                                          // AttackSO의 넉백 값 등

    private void Awake()
    {
        UpdateCharacterStat(); // 캐릭터 스탯을 업데이트(능력치 적용)하고 시작
    }

    private void UpdateCharacterStat() // 기본 능력치 세팅
    {
        AttackSO attackSO = null; 
        if (baseStat.attackSO != null) // baseStat을 활용해서 AttackSO와 CurrentStat 초기화
        {
            attackSO = Instantiate(baseStat.attackSO); // baseStat의 attackSO를 독립된 객체로 복사함
        }

        CurrentStat = new CharacterStat { attackSO = attackSO }; // 현재 능력치 세팅. 초기 값이기 때문에 현재 가지고 있는 attackSO를 넣어 기본 스탯을 복사한다
        // TODO :: 지금은 기본 능력치만 적용되지만, 앞으로는 능력치 강화 기능이 적용된다.

        CurrentStat.statsChangeType = baseStat.statsChangeType; // 추후 바뀔 값이라 위에서 생성할 때 포함 안함
        CurrentStat.maxHealth = baseStat.maxHealth;
        CurrentStat.speed = baseStat.speed;
    }
}