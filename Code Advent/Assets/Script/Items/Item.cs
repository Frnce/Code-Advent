using UnityEngine;
using Advent.InventorySystem;

namespace Advent.Items
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
    public class Item : ScriptableObject
    {
        new public string name = "New Item";
        public Sprite icon = null;
        public bool isDefault = false;
        public Rarity rarity;
        public int dropRarity;
        public GameObject itemObject;

        public virtual void Use()
        {
            //use the item
            Debug.Log("Using" + name);
        }
        public void RemoveFromInventory()
        {
            Inventory.instance.Remove(this);
        }
    }

}