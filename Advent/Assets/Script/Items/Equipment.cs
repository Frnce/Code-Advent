using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Advent.Inventories;
using Advent.Utilities;

namespace Advent.Items
{
    [CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
    public class Equipment : Item
    {
        public EquipSlot equipSlot;
        public WeaponType weaponType;
        public IntRange weaponRange = new IntRange(1, 1);

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
}