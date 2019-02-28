using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Advent.CharacterClass;

namespace Advent.CharacterStat
{
    public class CharacterStats : MonoBehaviour
    {
        public CharacterClasses characterClasses;

        public Stat health;
        public int currentHealth { get; set; }

        public Stat stamina;
        public int currentStamina { get; set; }
        public Stat attack;
        public Stat defense;
        public Stat speed;

        // remove ? instead of having min maxing abouts stats . you can have unique weapons with unique modifiers
        public Stat str;
        public Stat dex;
        public Stat vit;
        public Stat energy;

        private void Awake()
        {
            //Set base stats
            currentHealth = health.GetValue();
            currentStamina = stamina.GetValue();

            str = characterClasses.baseStr;
            dex = characterClasses.baseDex;
            vit = characterClasses.baseVit;
            energy = characterClasses.baseEne;
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