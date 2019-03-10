using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Advent.Items;

namespace Advent.UI
{
    public class InventorySlot : MonoBehaviour
    {
        Item item;
        public Image icon;
        public void AddItem(Item newItem)
        {
            item = newItem;

            icon.sprite = item.icon;
            icon.enabled = true;
        }
        public void ClearSlot()
        {
            item = null;
            icon.sprite = null;
            icon.enabled = false;
        }

        //TODO Add remove Item that drops to the ground

        public void UseItem()
        {
            if(item != null)
            {
                item.Use();
            }
        }
    }
}