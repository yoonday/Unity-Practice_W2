using UnityEngine;

public enum StatsChangeType
{
    Add, // 0
    Multiple, // 1
    Override // 2
}

// 데이터 폴더처럼 사용할 수 있게 해주는 Attribute
[System.Serializable]
public class CharacterStat // 스탯과 관련된 데이터. 스탯 혼자 행동하지 않으므로  System.Serializable을 사용하여 인스펙터 창에서 클래스의 내용을 확인 가능하게 함
{
    public StatsChangeType statsChangeType; // 스탯의 변화 값을 다양한 타입으로 주고자 함 → enum으로 관리
    [Range(1, 100)] public int maxHealth;  // [Range(1, 100)] 슬라이더가 생김
    [Range(1f, 20f)] public float speed;
    public AttackSO attackSO; // 캐릭터 스탯에 어떤 AttackSo가 들어있는지
}