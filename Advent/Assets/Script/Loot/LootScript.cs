using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent.Loot
{
    public class LootScript : MonoBehaviour
    {
        public LootTable lootTable;
        public int dropChance;


        public void DropLoot()
        {
            CalculateLoot();
        }
        void CalculateLoot()
        {
            int calculateDropChance = Random.Range(0, 101);

            if(calculateDropChance > dropChance)
            {
                Debug.Log("No loot");
                return;
            }
            if (calculateDropChance <= dropChance)
            {
                int itemWeight = 0;

                for (int i = 0; i < lootTable.items.Count; i++)
                {
                    itemWeight += lootTable.items[i].dropRate;
                }
                Debug.Log("Item Weight : " + itemWeight);

                int randomDropValue = Random.Range(0, itemWeight);

                for (int i = 0; i < lootTable.items.Count; i++)
                {
                    if(randomDropValue <= lootTable.items[i].dropRate)
                    {
                        Instantiate(lootTable.items[i].gameobject, transform.position, Quaternion.identity);
                        return;
                    }
                    randomDropValue -= lootTable.items[i].dropRate; //increased drop rate
                }
            }
        }
    }

}