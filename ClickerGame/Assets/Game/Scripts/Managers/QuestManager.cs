using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
public class QuestManager : MonoBehaviour
{
    [Header("Quest Pool")]
    public QuestData questData;

    [Header("UI")]
    public Text[] questTexts;

    [Header("Settings")]
    public int maxActiveQuests = 3;
    public float refreshInterval = 300f; // 5 minutes

    private List<QuestSO> activeQuests = new List<QuestSO>();
    private float refreshTimer;

    [Header("Dependencies")]
    public Clicker clicker;

    public delegate void CustomQuestCallback(QuestSO quest);
    public event CustomQuestCallback OnCustomQuestProgress;

    void Start()
    {
        RefreshQuests();
    }

    void Update()
    {
        refreshTimer += Time.deltaTime;
        if (refreshTimer >= refreshInterval || AllQuestsCompleted())
        {
            RefreshQuests();
            refreshTimer = 0f;
        }

        UpdateUI();
    }

    void UpdateUI()
    {
        for (int i = 0; i < questTexts.Length; i++)
        {
            if (i < activeQuests.Count)
            {
                var q = activeQuests[i];
                questTexts[i].text = $"{q.description} [{q.progress}/{q.targetValue}] {(q.isCompleted ? "(Done)" : "")}";
            }
            else
            {
                questTexts[i].text = "-";
            }
        }
    }

    void RefreshQuests()
    {
        activeQuests.Clear();

        List<QuestSO> pool = new List<QuestSO>(questData.quests);

        for (int i = 0; i < maxActiveQuests && pool.Count > 0; i++)
        {
            int index = Random.Range(0, pool.Count);
            QuestSO original = pool[index];

            QuestSO instance = Instantiate(original);
            instance.progress = 0;
            instance.isCompleted = false;

            activeQuests.Add(instance);
            pool.RemoveAt(index);
        }
    }

    public void OnClicked()
    {
        foreach (var quest in activeQuests)
        {
            if (quest.questType == QuestType.ClickCount)
                ProgressQuest(quest);
        }
    }

    public void OnItemPurchased()
    {
        foreach (var quest in activeQuests)
        {
            if (quest.isCompleted) continue;

            if (quest.questType == QuestType.BuyItem)
            {
                quest.progress++;
                if (quest.progress >= quest.targetValue)
                    quest.isCompleted = true;
            }
        }
    }

    void ProgressQuest(QuestSO quest, int amount = 1)
    {
        if (quest.isCompleted) return;

        if (quest.questType == QuestType.Custom)
        {
            OnCustomQuestProgress?.Invoke(quest);
            return;
        }

        quest.progress += amount;
        if (quest.progress >= quest.targetValue)
            CompleteQuest(quest);
    }

    bool AllQuestsCompleted()
    {
        foreach (var q in activeQuests)
            if (!q.isCompleted)
                return false;
        return true;
    }

    void CompleteQuest(QuestSO quest)
    {
        quest.isCompleted = true;
        clicker.totalClick += quest.rewardAmount;
        Debug.Log($"Quest '{quest.questName}' completed! +{quest.rewardAmount} clicks");
    }
}
