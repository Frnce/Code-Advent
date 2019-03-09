using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Advent.Player;
namespace Advent.Enemies
{
    public class MeleeTypeEnemy : Enemy
    {
        // Start is called before the first frame update
        public override void Start()
        {
            regenRate = .5f;
            maxStamina = 100f;

            base.Start();
            terminalSpeed = speed.GetValue() / 10;
            initialSpeed = (speed.GetValue() / 10) / 2;
            acceleration = (speed.GetValue() / 10) / 4;
        }
        public override HashSet<KeyValuePair<string, object>> createGoalState()
        {
            HashSet<KeyValuePair<string, object>> goal = new HashSet<KeyValuePair<string, object>>
            {
                new KeyValuePair<string, object>("damagePlayer", true),
                new KeyValuePair<string, object>("stayAlive", true)
            };
            return goal;
        }

        public override void PassiveRegen()
        {
            stamina += regenRate;
        }
        public override void Die()
        {
            base.Die();
            Destroy(gameObject);
        }
    }

}