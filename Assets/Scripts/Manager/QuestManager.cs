using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SceneManagement;
using UnityEngine;

public class QuestManager : MonoBehaviour
{

    // Q1 [구현사항 1]
    private static QuestManager instance;

    // Q3. [구현사항3]
    public List<QuestDataSO> Quests;

    // Q3. [선택 구현사항]
    public TextMeshProUGUI questInfo;

    // Q2 [구현사항 2]
    public static QuestManager Instance
    {

        get // 값을 읽을 때
        {
            if (instance == null) // instance가 null인지 확인하고
            {
                instance = FindObjectOfType<QuestManager>(); // null이라면 QuestManager 타입을 가진 컴포넌트를 찾아서 넣으세요

                if (instance == null) // 위의 과정에도 불구하고 null이라면
                {
                    // 새로운 게임오브젝트를 생성하세요
                    GameObject gameObject = new GameObject(nameof(QuestManager)); // 이름만 'QuestManager'임
                    instance = gameObject.AddComponent<QuestManager>();
                }

            }
            
            
            return instance;
        }
        
    }

    

    // Q1[구현사항 3] 인스턴스 검사 로직
    private void Awake()
    {
        if (Instance != null) Destroy(gameObject); // 이미 생성된 인스턴스가 있다면 새로 생성된 게임 오브젝트 파괴하기

        instance = this;
        PrintQuestData();
    }

    private void PrintQuestData()
    {
        string questText ="";
        
        for (int i = 0; i < Quests.Count; i++)
        {
            QuestDataSO quest = Quests[i];
            questText += $"Quest{i + 1} - {quest.QuestName} (최소 레벨 {quest.QuestRequiredLevel})";

            // Q3. [선택 구현사항]
            switch (quest)
            {
                case MonsterQuestDataSO monsterQuest:
                    questText += $"{monsterQuest.monsterName}를 {monsterQuest.headCount} 소탕";
                    break;
                case EncounterQuestDataSO encounterQuest:
                    questText += $"{encounterQuest.encounterName}과 대화하기";
                    break;
                default:
                    break;
            }
        }

        questInfo.text = questText;
    }
}
