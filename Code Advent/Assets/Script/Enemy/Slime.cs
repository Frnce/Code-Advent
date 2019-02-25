using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Advent.Player;
using Advent.EnemyStat;

namespace Advent.Enemies
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Slime : Enemy
    {

        Rigidbody2D rb2d;

        private void Awake()
        {
            
        }
        void Start()
        {

        }
        public override void Die()
        {
            base.Die();
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