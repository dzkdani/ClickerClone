using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Clicker/Item Data")]
public class ItemData : ScriptableObject
{
    public List<ItemSO> items;
}
