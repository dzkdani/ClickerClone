using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Clicker : MonoBehaviour
{
    public int clickValue = 1;
    public float autoClickRate = 1f;
    public int extraPerClick = 0;
    public int lastClickAmount { get; private set; } 
    private float autoClickTimer;

    public Text clickText;
    public int totalClick;


    void Start()
    {
        LoadData();
    }

    void Update()
    {
        autoClickTimer += Time.deltaTime;
        if (autoClickTimer >= 1f)
        {
            totalClick += Mathf.FloorToInt(autoClickRate);
            UpdateUI();
            autoClickTimer = 0f;
        }
    }

    public void OnClick()
    {
        lastClickAmount = clickValue + Mathf.FloorToInt(extraPerClick);

        FindFirstObjectByType<QuestManager>()?.OnClicked();

        totalClick += lastClickAmount;
        UpdateUI();
    }

    void UpdateUI()
    {
        clickText.text = $"{totalClick}";
    }

    public void SaveData()
    {
        SaveManager.LocalSaveInt(SaveKeys.ClickValue, clickValue);
        SaveManager.LocalSaveInt(SaveKeys.TotalClick, totalClick);
        SaveManager.LocalSaveFloat(SaveKeys.AutoClick, autoClickRate);
        SaveManager.localSave();
    }

    public void LoadData()
    {
        SaveManager.LocalLoadInt(SaveKeys.ClickValue, 1);
        SaveManager.LocalLoadInt(SaveKeys.TotalClick, 0);
        SaveManager.LocalLoadFloat(SaveKeys.AutoClick, 0f);
        UpdateUI();
    }
}

