using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Advent.CharacterClass;

namespace Advent.CharacterStats
{
    public class CharacterStats : MonoBehaviour
    {
        public CharacterClasses characterClasses;

        public Stat health;
        public int currentHealth { get; set; }

        public Stat mana;
        public int currentMana { get; set; }
        public Stat attack;
        public Stat defense;
        public Stat speed;

        public Stat str;
        public Stat dex;
        public Stat intelligence;
        public Stat con;

        private void Awake()
        {
            //Set base stats
            currentHealth = health.GetValue();
            currentMana = mana.GetValue();

            str = characterClasses.baseStr;
            dex = characterClasses.baseDex;
            intelligence = characterClasses.baseInt;
            con = characterClasses.baseCon;
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