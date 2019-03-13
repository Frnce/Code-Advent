using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Advent.Items;

namespace Advent.Loot
{
    [CreateAssetMenu(fileName = "New Loot Table", menuName = "Loot Table")]
    public class LootTable : ScriptableObject
    {
        public List<Item> items = new List<Item>();
        public int maxDrop;
    }
}