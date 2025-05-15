using UnityEngine;

public enum QuestType
{
    ClickCount,
    BuyItem,
    ClickForSeconds,
    Custom
}

[CreateAssetMenu(menuName = "Clicker/Quest")]
public class QuestSO : ScriptableObject
{
    public string questName;
    public QuestType questType;
    public int targetValue;
    public int rewardAmount; // Cookies awarded
    public string description;

    [HideInInspector] public int progress;
    [HideInInspector] public bool isCompleted;
}
