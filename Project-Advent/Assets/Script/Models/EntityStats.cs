using System.Collections;
using System.Collections.Generic;
using SA.enums;
using UnityEngine;

namespace SA.models
{
    [System.Serializable]
    public class EntityStats
    {
        public Jobs job;

        public string Name;
        public string description;

        public int strength;
        public int dexterity;
        public int intelligence;
        public int vitality;

        public EntityAttributes Attributes;

        public int baseAttack;
        public int curAttack;

        public int baseDefense;
        public int curDefense;
        public float elementalAttack;
        public float elementalDefense;
    }

}