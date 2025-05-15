using UnityEngine;

public enum ItemType
{
    AutoClick,
    ClickMultiplier,
    ExtraClick,
    Cosmetic
}

[CreateAssetMenu(menuName = "Clicker/Item")]
public class ItemSO : ScriptableObject
{
    public string itemName;
    public string description;
    public int baseCost;
    public ItemType itemType;
    public int effectValue;
    public GameObject cosmeticPrefabToEnable;
}
