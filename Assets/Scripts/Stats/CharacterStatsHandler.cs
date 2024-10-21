// �ɷ�ġ�� ���ϴ� �ͱ��� ������

using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatHandler : MonoBehaviour // Unity�� ������Ʈ �ý����� ����ϱ� ���ؼ� MonoBehaviour�� ��� �޴´�
{
    // �⺻ ���Ȱ� �߰� ������ ����ؼ� ���� ������ ����ϴ� ����
    // ������ �⺻ ���ȸ� ����

    [SerializeField] private CharacterStat baseStat; // ĳ���� ���� ������ ����
    public CharacterStat CurrentStat { get; private set; } // ���� �ɷ�ġ ���� �� ���� ���� ������ ��
    
    public List<CharacterStat> statModifiers = new List<CharacterStat>(); // � �߰� ����(����)�� �ִ��� ��Ƴ��� ����Ʈ ����
                                                                          // AttackSO�� �˹� �� ��

    private void Awake()
    {
        UpdateCharacterStat(); // ĳ���� ������ ������Ʈ(�ɷ�ġ ����)�ϰ� ����
    }

    private void UpdateCharacterStat() // �⺻ �ɷ�ġ ����
    {
        AttackSO attackSO = null; 
        if (baseStat.attackSO != null) // baseStat�� Ȱ���ؼ� AttackSO�� CurrentStat �ʱ�ȭ
        {
            attackSO = Instantiate(baseStat.attackSO); // baseStat�� attackSO�� ������ ��ü�� ������
        }

        CurrentStat = new CharacterStat { attackSO = attackSO }; // ���� �ɷ�ġ ����. �ʱ� ���̱� ������ ���� ������ �ִ� attackSO�� �־� �⺻ ������ �����Ѵ�
        // TODO :: ������ �⺻ �ɷ�ġ�� ���������, �����δ� �ɷ�ġ ��ȭ ����� ����ȴ�.

        CurrentStat.statsChangeType = baseStat.statsChangeType; // ���� �ٲ� ���̶� ������ ������ �� ���� ����
        CurrentStat.maxHealth = baseStat.maxHealth;
        CurrentStat.speed = baseStat.speed;
    }
}