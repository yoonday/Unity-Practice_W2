using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestSO", menuName = "QuestData/Type/Encounter", order = 2)]
public class EncounterQuestDataSO : QuestDataSO
{
    [Header("Encounter Info")]
    public string encounterName;
}
