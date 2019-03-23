using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Advent.Inventories;

namespace Advent.Items
{
    [CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
    public class Equipment : Item
    {
        public EquipSlot equipSlot;

        public int defenseModifier;
        public int pAttackModifier;

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
    }
    public enum EquipSlot
    {
        HEAD,
        BODY,
        ACCESORY,
        WEAPON
    }
}