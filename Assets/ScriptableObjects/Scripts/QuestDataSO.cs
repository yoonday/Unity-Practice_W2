using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Q3 [�������� 1]
[CreateAssetMenu(fileName = "DefaultQuestSO", menuName = "QuestData", order = 0)]
public class QuestDataSO : ScriptableObject
{
    [Header("Quest Info")]
    public string QuestName;
    public int QuestRequiredLevel;
    public int QuestNPC;
    public List<int> QestPrerequisites;
}
