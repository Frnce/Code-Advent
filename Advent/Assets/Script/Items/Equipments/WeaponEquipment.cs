using Advent.Inventories;
using Advent.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Advent.Entities;

namespace Advent.Items
{
    [CreateAssetMenu(fileName = "New Weapon", menuName = "Inventory/Equipment/Weapon")]
    public class WeaponEquipment : Equipment
    {
        public WeaponEquipment()
        {
            equipSlot = EquipSlot.WEAPON;
        }
        public WeaponType weaponType;
        public IntRange weaponRange = new IntRange(1, 1);
        // Start is called before the first frame update
        public override void Use()
        {
            base.Use();
            EquipmentManager.instance.Equip(this);
            SetIsRange();
            RemoveFromInventory();
        }
        private void SetIsRange()
        {
            if (weaponType == WeaponType.RANGE)
            {
                Player.instance.canRangeSingleAttack = true;
                Player.instance.rangeOfWeapon = weaponRange;
            }
            else
            {
                Player.instance.canRangeSingleAttack = false;
            }
        }
    }
}