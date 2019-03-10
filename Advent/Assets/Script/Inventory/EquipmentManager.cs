using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Advent.Items;

namespace Advent.Inventories
{
    public class EquipmentManager : MonoBehaviour
    {
        #region Singleton
        public static EquipmentManager instance;
        private void Awake()
        {
            if(instance != null)
            {
                Debug.LogWarning("More than one equipmentManager of inventory found!");
            }
            instance = this;
        }
        #endregion

        public Equipment[] currentEquipment;
        public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
        public OnEquipmentChanged onEquipmentChanged;
        Inventory inventory;
        // Start is called before the first frame update
        void Start()
        {
            inventory = Inventory.instance;
            int numOfSlots = System.Enum.GetNames(typeof(EquipSlot)).Length;
            currentEquipment = new Equipment[numOfSlots];
        }

        public void Equip(Equipment newItem)
        {
            int slotIndex = (int)newItem.equipSlot;
            Equipment oldItem = null;
            if(currentEquipment[slotIndex] != null)
            {
                oldItem = currentEquipment[slotIndex];
                inventory.AddItem(oldItem);
            }

            if(onEquipmentChanged != null)
            {
                onEquipmentChanged.Invoke(newItem, oldItem);
            }

            currentEquipment[slotIndex] = newItem;
        }
        public void Unequip(int slotIndex)
        {
            if(currentEquipment[slotIndex] != null)
            {
                Equipment oldItem = currentEquipment[slotIndex];
                inventory.AddItem(oldItem);
                currentEquipment[slotIndex] = null;

                if (onEquipmentChanged != null)
                {
                    onEquipmentChanged.Invoke(null, oldItem);
                }
            }
        }

        public void UnequipAll()
        {
            for (int i = 0; i < currentEquipment.Length; i++)
            {
                Unequip(i);
            }
        }
    }
}