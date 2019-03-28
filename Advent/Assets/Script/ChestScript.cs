using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Advent.Loot;

namespace Advent
{
    public class ChestScript : MonoBehaviour
    {
        private LootScript lootScript;
        bool isNearChest;
        // Start is called before the first frame update
        void Start()
        {
            lootScript = GetComponent<LootScript>();
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.CompareTag("Player"))
            {
                Debug.Log("Chest Open");
            }
        }
        public void OpenChest()
        {
            lootScript.DropLoot();
            Destroy(gameObject);
        }
    }

}