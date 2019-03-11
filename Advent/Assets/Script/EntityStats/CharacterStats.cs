using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Advent.Character;
using System;

namespace Advent.Stats
{
    public class CharacterStats : MonoBehaviour
    {
        public CharacterClass character;

        public int MaxHealth { get; set; }
        public int MaxStamina { get; set; }
        public int CurrentHealth { get; set; }
        public int CurrentStamina { get; set; }

        public Stat physicalAttack;
        public Stat magicalAttack;
        public Stat defense;
        public Stat strength;
        public Stat dexterity;
        public Stat intelligence;
        public Stat vitality;
        public Stat speed;

        public int availablePoints;

        public void InitStats()
        {
            strength.AddStat(character.baseStr);
            dexterity.AddStat(character.baseDex);
            intelligence.AddStat(character.baseInt);
            vitality.AddStat(character.baseVit);
            speed.AddStat(character.baseSpeed);

            SetHP();
            SetST();
            SetStat();
            CurrentHealth = MaxHealth;
            CurrentStamina = MaxStamina;
        }
        public void SetStat()
        {
            physicalAttack.AddStat(strength.GetValue());
        }
        public void SetHP() //Set new max HP when updating vit stat // Use this method after inserting a new stat on VIT
        {
            MaxHealth = 3 * (LevelSystemController.instance.currentLevel + vitality.GetValue());
        }
        public void SetST() //Set new max ST when updating int stat // // Use this method after inserting a new stat on INT
        {
            MaxStamina = 2 * (LevelSystemController.instance.currentLevel + intelligence.GetValue());
        }
        public void TakeDamage(int damage)
        {
            damage -= defense.GetValue();
            damage = Mathf.Clamp(damage, 0, int.MaxValue); //have room for improvements ,. ,balancing shits

            CurrentHealth -= damage;
            Debug.Log("Take Damage" + transform.name);

            if (CurrentHealth <= 0)
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