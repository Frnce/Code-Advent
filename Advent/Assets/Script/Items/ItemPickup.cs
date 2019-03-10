using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Advent.Inventories;

namespace Advent.Items
{
    public class ItemPickup : MonoBehaviour
    {
        public Item item;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                Debug.Log("Item Spotted");
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Pickup();
                }
            }
        }
        void Pickup()
        {
            Debug.Log("Picked up item " + item.name);
            bool isPickedUp = Inventory.instance.AddItem(item);

            if (isPickedUp)
            {
                Destroy(gameObject);
            }
        }
    }
}