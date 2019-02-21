using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Advent.InventorySystem;

namespace Advent.Items
{
    public class ItemPickup : MonoBehaviour
    {
        public Item item;
        void Pickup()
        {
            //Temp , Can be used when the item is picked up , the stat modifier will be used;
            Debug.Log("Item PIcked " + item.name);
            Inventory.instance.Add(item);
            Destroy(gameObject);
        }
        private void OnTriggerStay2D(Collider2D collision)
        {
            if (Input.GetKeyDown(KeyCode.F) && collision.CompareTag("Player"))
            {
                Pickup();
                if (item != null)
                {
                    item.Use();
                }
            }
        }
    }

}