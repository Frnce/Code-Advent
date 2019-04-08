using Advent.Inventories;
using Advent.Utilities;
using UnityEngine;

namespace Advent.Items
{
    [CreateAssetMenu(fileName = "New Head", menuName = "Inventory/Equipment/Head")]
    public class HeadEquipment : Equipment
    {
        public HeadEquipment()
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