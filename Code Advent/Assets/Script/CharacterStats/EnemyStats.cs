using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Advent.CharacterStat;

namespace Advent.EnemyStat
{
    public class EnemyStats : MonoBehaviour
    {
        public Stat health;
        public int currentHealth { get; set; }

        public Stat attack;
        public Stat defense;
        public Stat speed;

        public Stat str;
        public Stat dex;
        public Stat intelligence;
        public Stat con;

        private void Awake()
        {
            currentHealth = health.GetValue();
        }
        public void TakeDamage(int damage)
        {
            damage -= defense.GetValue();
            damage = Mathf.Clamp(damage, 0, int.MaxValue); //have room for improvements ,. ,balancing shits

            currentHealth -= damage;
            Debug.Log("Take Damage");

            if (currentHealth <= 0)
            {
                Die();
            }
        }
        public virtual void Die()
        {
            //Die in some way 
            //meant to be overwritten
            Debug.Log("Died");
        }
    }

}