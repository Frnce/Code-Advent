using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Advent.Stats
{
    public class CharacterStats : MonoBehaviour
    {
        [Header("HP and ST")]
        public Stat health;
        public int currentHealth { get; set; }

        public Stat stamina;
        public int currentStamina { get; set; }

        [Header("Attack and Def")]
        public Stat physicalAttack;
        public Stat magicalAttack;
        public Stat defense;

        [Header("Stats")]
        public Stat strength;
        public Stat dexterity;
        public Stat intelligence;
        public Stat vitality;
        public Stat speed;

        private void Awake()
        {
            currentHealth = health.GetValue();
            currentStamina = stamina.GetValue();
        }
        public void TakeDamage(int damage)
        {
            damage -= defense.GetValue();
            damage = Mathf.Clamp(damage, 0, int.MaxValue); //have room for improvements ,. ,balancing shits

            currentHealth -= damage;
            Debug.Log("Take Damage" + transform.name);

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