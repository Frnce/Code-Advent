using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Advent.Stats;

namespace Advent.Enemies
{
    public class SkeletonEnemy : MeleeEnemy
    {
        LevelSystemController levelSystem;
        // Start is called before the first frame update
        public override void Start()
        {
            levelSystem = LevelSystemController.instance;
            anim = GetComponent<Animator>();
            base.Start();
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
            levelSystem.GainExp(character.expGiven);
            Destroy(gameObject);
        }
    }

}