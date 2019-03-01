using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Advent.Items;

namespace Advent.Loot
{
    [CreateAssetMenu(fileName = "New Loots Table", menuName = "Loot Table")]
    public class LootTable : ScriptableObject
    {
        public List<Item> items = new List<Item>();

        public GameObject coinObject;
        public int minCoinDrop;
        public int maxCoinDrop;

        public int maxItemDrop;
    }
}