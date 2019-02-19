using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Loots Table",menuName = "Loot Table")]
public class LootTable : ScriptableObject
{
    public List<Item> items = new List<Item>();
    public int maxItemDrop;
}
