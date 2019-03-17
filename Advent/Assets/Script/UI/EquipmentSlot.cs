using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Advent.Items;
using UnityEngine.UI;

namespace Advent.UI
{
    public class EquipmentSlot : MonoBehaviour
    {
        Equipment equipment;
        public Image icon;
        public int thisIndex;
        public void AddItem(Equipment newItem,Equipment oldItem)
        {
            equipment = newItem;

            icon.sprite = newItem.icon;
            icon.enabled = true;
        }
        public void ClearSlot()
        {
            equipment = null;
            icon.sprite = null;
            icon.enabled = false;
        }
        public void UseItem()
        {
            if (equipment != null)
            {
                equipment.Use();
            }
        }
        public void UnequipItem()
        {
            if(equipment != null)
            {
                equipment.Unequip(thisIndex);
                ClearSlot();
            }
        }
    }
}