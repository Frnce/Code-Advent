using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Advent.Inventories;
using Advent.UI;

namespace Advent.Items
{
    public class ItemPickup : MonoBehaviour
    {
        public Item item;
        private bool isStepOn = false; //Checks if Player is inside the tile of the item
        private EventLogs eventlogs;
        // Start is called before the first frame update
        void Start()
        {
            eventlogs = FindObjectOfType<EventLogs>();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.F) && isStepOn)
            {
                Pickup();
            }
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                eventlogs.AddEvent(item.name + " is on the floor.");
                isStepOn = true;
            }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                isStepOn = false;
            }
        }
        void Pickup()
        {
            eventlogs.AddEvent("Picked up " + item.name);
            bool isPickedUp = Inventory.instance.AddItem(item);

            if (isPickedUp)
            {
                Destroy(gameObject);
            }
        }
    }
}