using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Advent.Player;
using Advent.EnemyStat;
using Advent.Loot;

namespace Advent.Enemies
{
    [RequireComponent(typeof(Rigidbody2D),typeof(LootScript))]
    public class Slime : Enemy
    {

        Rigidbody2D rb2d;
        LootScript loot;

        private void Awake()
        {
            player = PlayerController.instance;
            loot = GetComponent<LootScript>();
        }
        void Start()
        {
            maxStamina = stamina;

            terminalSpeed = speed.GetValue() / 10;
            initialSpeed = (speed.GetValue() / 10) / 2;
            acceleration = (speed.GetValue() / 10) / 4;
        }
        public override void Die()
        {
            base.Die();
            //Drop loot here
            loot.CalculateLoot();
            Destroy(gameObject);
        }

        public override void PassiveRegen()
        {
            stamina += regenRate;
        }

        public override HashSet<KeyValuePair<string, object>> createGoalState()
        {
            HashSet<KeyValuePair<string, object>> goal = new HashSet<KeyValuePair<string, object>>();
            goal.Add(new KeyValuePair<string, object>("damagePlayer", true));
            goal.Add(new KeyValuePair<string, object>("stayAlive", true));
            return goal;
        }
    }
}