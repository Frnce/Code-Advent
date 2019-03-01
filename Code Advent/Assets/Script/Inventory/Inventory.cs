using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Advent.Items;

namespace Advent.InventorySystem
{
    public class Inventory : MonoBehaviour
    {
        #region Singleton
        public static Inventory instance;

        private void Awake()
        {
            if (instance != null)
            {
                Debug.LogWarning("More than one instance of inventory found");
                return;
            }
            instance = this;
        }
        #endregion
        public delegate void OnItemChanged();
        public OnItemChanged onItemChangedCallBack;
        public List<Item> items = new List<Item>();

        public void Add(Item item)
        {
            if (!item.isDefault)
            {
                items.Add(item);
                if (onItemChangedCallBack != null)
                {
                    onItemChangedCallBack.Invoke();
                }
            }
        }
        public void Remove(Item item)
        {
            items.Remove(item);
            if (onItemChangedCallBack != null)
            {
                onItemChangedCallBack.Invoke();
            }
        }

        public int maxCoins;
        public void AddCoins(int coins)
        {
            maxCoins += coins;
        }
        public void RemoveCoins(int coins)
        {
            maxCoins -= coins;
        }
    }

}