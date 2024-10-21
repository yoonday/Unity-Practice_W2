using UnityEngine;

public enum StatsChangeType
{
    Add, // 0
    Multiple, // 1
    Override // 2
}

// ������ ����ó�� ����� �� �ְ� ���ִ� Attribute
[System.Serializable]
public class CharacterStat // ���Ȱ� ���õ� ������. ���� ȥ�� �ൿ���� �����Ƿ�  System.Serializable�� ����Ͽ� �ν����� â���� Ŭ������ ������ Ȯ�� �����ϰ� ��
{
    public StatsChangeType statsChangeType; // ������ ��ȭ ���� �پ��� Ÿ������ �ְ��� �� �� enum���� ����
    [Range(1, 100)] public int maxHealth;  // [Range(1, 100)] �����̴��� ����
    [Range(1f, 20f)] public float speed;
    public AttackSO attackSO; // ĳ���� ���ȿ� � AttackSo�� ����ִ���
}