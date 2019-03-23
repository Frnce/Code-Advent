﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Advent.Inventories;
using Advent.Items;
using System;

namespace Advent.UI
{
    public class ShowInventory : MonoBehaviour
    {
        public GameObject inventoryPanel;
        public GameObject equipmentPanel;

        public Transform itemsParent;
        public Transform equipParent;

        private PlayerController player;
        private Inventory inventory;
        private InventorySlot[] itemSlot;
        private EquipmentManager equipmentManager;
        private EquipmentSlot[] equipmentSlot;

        // Start is called before the first frame update
        void Start()
        {
            player = PlayerController.instance;

            inventory = Inventory.instance;
            inventory.onItemChangedCallback += UpdateInventoryUI;

            equipmentManager = EquipmentManager.instance;
            equipmentManager.onEquipmentChanged += UpdateEquipmentUI;

            itemSlot = itemsParent.GetComponentsInChildren<InventorySlot>();
            equipmentSlot = equipParent.GetComponentsInChildren<EquipmentSlot>();
        }
        // Update is called once per frame
        void Update()
        {

        }
        void UpdateInventoryUI()
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
        void UpdateEquipmentUI(Equipment newItem, Equipment oldItem)
        {
            if(newItem != null)
            {
                int slotIndex = (int)newItem.equipSlot;
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