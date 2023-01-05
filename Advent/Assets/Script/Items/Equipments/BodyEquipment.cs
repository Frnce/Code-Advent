using Advent.Inventories;
using Advent.Utilities;
using UnityEngine;

namespace Advent.Items
{
    [CreateAssetMenu(fileName = "New Body", menuName = "Inventory/Equipment/Body")]
    public class BodyEquipment : Equipment
    {
        public BodyEquipment()
        {
            equipSlot = EquipSlot.HEAD;
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