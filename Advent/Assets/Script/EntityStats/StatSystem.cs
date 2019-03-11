using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Advent.Inventories;
using Advent.Items;

namespace Advent.Stats
{
    public class StatSystem : CharacterStats
    {
        public static StatSystem instance;
        private void Awake()
        {
            instance = this;
        }
        // Start is called before the first frame update
        void Start()
        {
            InitStats();
            EquipmentManager.instance.onEquipmentChanged += OnEquipmentChanged;
        }

        // Update is called once per frame
        void Update()
        {
            SetHP();
            SetST();
        }
        void OnEquipmentChanged(Equipment newItem, Equipment oldItem)
        {
            if (newItem != null)
            {
                defense.AddModifier(newItem.defenseModifier);
                physicalAttack.AddModifier(newItem.pAttackModifier);
            }
            if (oldItem != null)
            {
                defense.RemoveModifier(oldItem.defenseModifier);
                physicalAttack.RemoveModifier(oldItem.pAttackModifier);
            }
        }
        //For Stat Button when leveled up
        public void AddPointsToStr()
        {
            strength.AddStat(1);
            physicalAttack.AddStat(1);
            availablePoints--;
        }
        public void AddPointsToDex()
        {
            dexterity.AddStat(1);
            availablePoints--;
        }
        public void AddPointsToInt()
        {
            intelligence.AddStat(1);
            availablePoints--;
        }
        public void AddPointsToVit()
        {
            vitality.AddStat(1);
            availablePoints--;
        }
    }
}