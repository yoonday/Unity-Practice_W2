using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SceneManagement;
using UnityEngine;

public class QuestManager : MonoBehaviour
{

    // Q1 [�������� 1]
    private static QuestManager instance;

    // Q3. [��������3]
    public List<QuestDataSO> Quests;

    // Q3. [���� ��������]
    public TextMeshProUGUI questInfo;

    // Q2 [�������� 2]
    public static QuestManager Instance
    {

        get // ���� ���� ��
        {
            if (instance == null) // instance�� null���� Ȯ���ϰ�
            {
                instance = FindObjectOfType<QuestManager>(); // null�̶�� QuestManager Ÿ���� ���� ������Ʈ�� ã�Ƽ� ��������

                if (instance == null) // ���� �������� �ұ��ϰ� null�̶��
                {
                    // ���ο� ���ӿ�����Ʈ�� �����ϼ���
                    GameObject gameObject = new GameObject(nameof(QuestManager)); // �̸��� 'QuestManager'��
                    instance = gameObject.AddComponent<QuestManager>();
                }

            }
            
            
            return instance;
        }
        
    }

    

    // Q1[�������� 3] �ν��Ͻ� �˻� ����
    private void Awake()
    {
        if (Instance != null) Destroy(gameObject); // �̹� ������ �ν��Ͻ��� �ִٸ� ���� ������ ���� ������Ʈ �ı��ϱ�

        instance = this;
        PrintQuestData();
    }

    private void PrintQuestData()
    {
        string questText ="";
        
        for (int i = 0; i < Quests.Count; i++)
        {
            QuestDataSO quest = Quests[i];
            questText += $"Quest{i + 1} - {quest.QuestName} (�ּ� ���� {quest.QuestRequiredLevel})";

            // Q3. [���� ��������]
            switch (quest)
            {
                case MonsterQuestDataSO monsterQuest:
                    questText += $"{monsterQuest.monsterName}�� {monsterQuest.headCount} ����";
                    break;
                case EncounterQuestDataSO encounterQuest:
                    questText += $"{encounterQuest.encounterName}�� ��ȭ�ϱ�";
                    break;
                default:
                    break;
            }
        }

        questInfo.text = questText;
    }
}
