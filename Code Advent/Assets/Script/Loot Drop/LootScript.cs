using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootScript : MonoBehaviour
{
    public LootTable lootTable;
    List<GameObject> droppedItems = new List<GameObject>();
    public void CalculateLoot()
    {
        for (int k = 0; k < lootTable.maxItemDrop; k++)
        {
            int itemWeight = 0;
            for (int i = 0; i < lootTable.items.Count; i++)
            {
                itemWeight += lootTable.items[i].dropRarity;
            }

            int randomValue = Random.Range(0, itemWeight);
            for (int j = 0; j < lootTable.items.Count; j++)
            {
                if (randomValue <= lootTable.items[j].dropRarity)
                {
                    GameObject item = Instantiate(lootTable.items[j].itemObject, transform.position, Quaternion.identity);
                    droppedItems.Add(item);
                    ScrambleItems(item);
                    break;
                }
                randomValue -= lootTable.items[j].dropRarity;
            }
        }
    }
    void ScrambleItems(GameObject item) // Scramble the positions of the items on a radius
    {
        float randomX = Mathf.Round(Random.Range(-3f, 3f));
        float randomY = Random.Range(-3f, 3f);
        Vector3 newPosition = transform.position + new Vector3(randomX, randomY);

        item.transform.position = newPosition;
    }
}
