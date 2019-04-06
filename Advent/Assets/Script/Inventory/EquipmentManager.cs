using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Advent.Items;
using Advent.Entities;
using Advent.Utilities;

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
        public OnEquipmentChanged onEquipmentChangedCallback;
        Player player;
        Inventory inventory;
        // Start is called before the first frame update
        void Start()
        {
            player = Player.instance;
            inventory = Inventory.instance;
            int numOfSlots = System.Enum.GetNames(typeof(EquipSlot)).Length;
            currentEquipment = new Equipment[numOfSlots];

            onEquipmentChangedCallback += OnEquipmentChange;
        }

        void OnEquipmentChange(Equipment newItem, Equipment oldItem)
        {
            if (newItem != null)
            {
                player.defense.AddModifier(newItem.defenseModifier);
                player.attack.AddModifier(newItem.pAttackModifier);

                if(newItem.weaponType == WeaponType.RANGE && newItem.equipSlot == EquipSlot.WEAPON)
                {
                    player.canRangeSingleAttack = true;
                }
                else
                {
                    player.canRangeSingleAttack = false;
                }
            }
            if (oldItem != null)
            {
                player.defense.RemoveModifier(oldItem.defenseModifier);
                player.attack.RemoveModifier(oldItem.pAttackModifier);
            }
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

            if(onEquipmentChangedCallback != null)
            {
                onEquipmentChangedCallback.Invoke(newItem, oldItem);
            }

            currentEquipment[slotIndex] = newItem;
        }
        public void SwapEquip(int slotIndex)
        {
            if(currentEquipment[slotIndex] != null)
            {
                Equipment oldItem = currentEquipment[slotIndex];
                inventory.AddItem(oldItem);
                currentEquipment[slotIndex] = null;

                if (onEquipmentChangedCallback != null)
                {
                    onEquipmentChangedCallback.Invoke(null, oldItem);
                }
            }
        }
        public void UnequipAll()
        {
            for (int i = 0; i < currentEquipment.Length; i++)
            {
                if(currentEquipment[i] != null)
                {
                    SwapEquip(i);
                }
            }
        }
    }
}