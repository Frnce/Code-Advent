using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Advent.InventorySystem;

namespace Advent
{
    public class CoinController : MonoBehaviour
    {
        private int value = 1;
        Inventory inventory;
        private void Start()
        {
            inventory = Inventory.instance;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                inventory.AddCoins(value);
                Destroy(gameObject);
            }
        }
    }
}