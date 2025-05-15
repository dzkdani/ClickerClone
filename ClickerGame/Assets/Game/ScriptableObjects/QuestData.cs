using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Clicker/Quest Data")]
public class QuestData : ScriptableObject
{
    public List<QuestSO> quests;
}
