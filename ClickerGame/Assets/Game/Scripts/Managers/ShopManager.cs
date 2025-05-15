using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class ShopManager : MonoBehaviour
{
    [Header("Item Pool")]
    public ItemData itemData;

    [Header("UI")]
    public Transform shopContainer;
    public GameObject buttonPrefab;

    [Header("Dependencies")]
    public Clicker clicker;

    private Dictionary<ItemSO, int> itemPurchaseCounts = new Dictionary<ItemSO, int>();

    void Start()
    {
        foreach (var item in itemData.items)
        {
            var btnObj = Instantiate(buttonPrefab, shopContainer);
            btnObj.GetComponentInChildren<TextMeshProUGUI>().text = $"{item.itemName} - {item.baseCost}";
            ItemSO currentItem = item;
            btnObj.GetComponent<Button>().onClick.AddListener(() => Buy(currentItem));
            itemPurchaseCounts[currentItem] = 0;
        }
    }

    public void Buy(ItemSO item)
    {
        if (clicker.totalClick >= item.baseCost)
        {
            clicker.totalClick -= item.baseCost;
            itemPurchaseCounts[item]++;
            ApplyItemEffect(item);
        }
        else
        {
            Debug.Log("Not enough clicks!");
        }
    }

    void ApplyItemEffect(ItemSO item)
    {
        switch (item.itemType)
        {
            case ItemType.AutoClick:
                clicker.autoClickRate += item.effectValue;
                break;

            case ItemType.ClickMultiplier:
                clicker.clickValue += item.effectValue;
                break;

            case ItemType.ExtraClick:
                clicker.extraPerClick += item.effectValue;
                break;

            case ItemType.Cosmetic:
                if (item.cosmeticPrefabToEnable != null)
                    item.cosmeticPrefabToEnable.SetActive(true);
                break;
        }
    }
}
