using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Advent.Inventories;
using Advent.Items;
using System;

namespace Advent.UI
{
    public class ShowInventory : MonoBehaviour
    {
        public Transform itemsParent;
        public Transform equipParent;

        private Inventory inventory;
        private InventorySlot[] itemSlot;
        private EquipmentManager equipmentManager;
        private EquipmentSlot[] equipmentSlot;
        void Start()
        {
            inventory = Inventory.instance;
            inventory.onItemChangedCallback += UpdateInventoryUI;

            equipmentManager = EquipmentManager.instance;
            equipmentManager.onEquipmentChangedCallback += UpdateEquipmentUI;

            itemSlot = itemsParent.GetComponentsInChildren<InventorySlot>();
            equipmentSlot = equipParent.GetComponentsInChildren<EquipmentSlot>();
        }
        public void UpdateInventoryUI()
        {
            for (int i = 0; i < itemSlot.Length; i++)
            {
                if( i < inventory.items.Count)
                {
                    itemSlot[i].AddItem(inventory.items[i]);
                }
                else
                {
                    itemSlot[i].ClearSlot();
                }
            }
        }
        public void UpdateEquipmentUI(Equipment newItem, Equipment oldItem)
        {
            if(newItem != null)
            {
                int slotIndex = (int)newItem.GetEquipSlot();
                for (int i = 0; i < equipmentSlot.Length; i++)
                {
                    if (i < equipmentManager.currentEquipment.Length)
                    {
                        equipmentSlot[slotIndex].AddItem(newItem, oldItem);
                    }
                    else
                    {
                        equipmentSlot[i].ClearSlot();
                    }
                }
            }
        }
    }
}