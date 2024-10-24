using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestSO", menuName = "QuestData/Type/Monster", order = 1)]
public class MonsterQuestDataSO : QuestDataSO
{
    [Header("Monster Info")]
    public string monsterName;
    public int headCount; 
}
