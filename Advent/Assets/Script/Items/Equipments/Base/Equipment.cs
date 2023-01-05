using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Advent.Inventories;
using Advent.Utilities;

namespace Advent.Items
{
    public class Equipment : Item
    {
        protected EquipSlot equipSlot;
        public EquipmentRarity rarity;
        public int defenseModifier;
        public int pAttackModifier; //TODO change to intrange
        //statsMod(class)[] statMod
        public override void Use()
        {
            base.Use();
            EquipmentManager.instance.Equip(this);
            RemoveFromInventory();
        }
        public void Unequip(int index)  
        {
            EquipmentManager.instance.SwapEquip(index);
        }
        public EquipSlot GetEquipSlot()
        {
            return equipSlot;
        }
    }
}