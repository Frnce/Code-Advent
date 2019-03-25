using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Advent.Entities;

namespace Advent.Items
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Consumable/HealthPotion")]
    public class HealthPotion : Item
    {
        private int healthValue = 10;

        public override void Use()
        {
            base.Use();
            if (Player.instance.currentHealth != Player.instance.maxHealth)
            {
                Player.instance.currentHealth += healthValue;
                if (Player.instance.currentHealth >= Player.instance.maxHealth)
                {
                    Player.instance.currentHealth = Player.instance.maxHealth;
                }
                RemoveFromInventory();
            }
        }
    }
}