using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Advent.Items
{
    public class EquipmentPickup : MonoBehaviour
    {
        public Equipment equipment;
        void Pickup()
        {
            //Temp , Can be used when the item is picked up , the stat modifier will be used;
            Debug.Log("Item PIcked " + equipment.name);
            Destroy(gameObject);
        }
        private void OnTriggerStay2D(Collider2D collision)
        {
            if (Input.GetKeyDown(KeyCode.F) && collision.CompareTag("Player"))
            {
                Pickup();
                if (equipment != null)
                {
                    equipment.Use();
                }
            }
        }
    }

}