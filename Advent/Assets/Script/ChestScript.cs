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

        // Update is called once per frame
        void Update()
        {
            if (isNearChest)
            {
                Debug.Log("near chest");
                if (Input.GetKeyDown(KeyCode.F))
                {
                    lootScript.DropLoot();
                    Destroy(gameObject);
                }
            }
        }
        private void OnTriggerEnter2D (Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                isNearChest = true;
            }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                isNearChest = false;
            }
        }
    }

}