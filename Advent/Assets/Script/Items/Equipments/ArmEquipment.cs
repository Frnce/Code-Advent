using Advent.Inventories;
using Advent.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent.Items
{
    [CreateAssetMenu(fileName = "New Arm", menuName = "Inventory/Equipment/Arm")]
    public class ArmEquipment : Equipment
    {
        public ArmEquipment()
        {
            equipSlot = EquipSlot.ACCESORY;
        }
        // Start is called before the first frame update
        public override void Use()
        {
            base.Use();
            EquipmentManager.instance.Equip(this);
            RemoveFromInventory();
        }
    }
}