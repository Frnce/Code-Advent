using UnityEngine;
using Advent.Inventories;

namespace Advent.Items
{
    [CreateAssetMenu(fileName ="New Item", menuName = "Inventory/Item")]
    public class Item : ScriptableObject
    {
        public float level; 
        new public string name = "New Item";
        public Sprite icon = null;
        public bool isDefaultItem = false;
        public GameObject gameobject;
        public int dropRate;

        public virtual void Use()
        {
            Debug.Log("Using " + name);
        }

        public void RemoveFromInventory()
        {
            Inventory.instance.RemoveItem(this);
        }
        public void DropFromInventory()
        {
            Debug.Log("Drop Item " + name);
            Inventory.instance.RemoveItem(this);
            Instantiate(gameobject,PlayerController.instance.transform.position, Quaternion.identity);
        }
    }
}