using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Advent.Player;
using Advent.Items;
using Advent.InventorySystem;

namespace Advent
{
    public class EquipmentManager : MonoBehaviour
    {
        #region Singleton
        public static EquipmentManager instance;
        private void Awake()
        {
            instance = this;
        }
        #endregion

        public Equipment[] defaultEquipment; 
        Equipment[] currentEquipment;

        public delegate void OnEquipmentChanged(Equipment oldItem, Equipment newItem);
        public OnEquipmentChanged onEquipmentChanged;
        Inventory inventory;

        private void Start()
        {
            inventory = Inventory.instance;
            int numOfSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
            currentEquipment = new Equipment[numOfSlots];

            EquipDefaultItems();
        }
        public void Equip(Equipment newItem) // Gets the equipment on the ground and put it on the equipment manager
        {
            int slotIndex = (int)newItem.equipmentSlot;

            Equipment oldItem = null;

            if (currentEquipment[slotIndex] != null)
            {
                oldItem = currentEquipment[slotIndex];
                Instantiate(oldItem.itemObject, PlayerController.instance.transform.position, Quaternion.identity); // instantiate the old equipped when replaced it with the newly equipped item
            }
            if (onEquipmentChanged != null)
            {
                onEquipmentChanged.Invoke(oldItem, newItem);
                //invokes delegate to get the item data
            }
            currentEquipment[slotIndex] = newItem;
            //Shows the currently Equipped on the player.
            PlayerController.instance.transform.GetChild(1).GetChild(0).GetComponent<SpriteRenderer>().sprite = newItem.icon;
        }
        public void Unequip(int slotIndex)
        {
            if (currentEquipment[slotIndex] != null)
            {
                Equipment oldItem = currentEquipment[slotIndex];
                inventory.Add(oldItem);

                currentEquipment[slotIndex] = null;
                if (onEquipmentChanged != null)
                {
                    onEquipmentChanged.Invoke(oldItem, null);
                }
            }
        }
        void EquipDefaultItems()
        {
            foreach (Equipment item in defaultEquipment)
            {
                Equip(item);
            }
        }
    }

}