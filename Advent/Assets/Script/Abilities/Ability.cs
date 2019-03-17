﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent.Abilities
{ 
    public abstract class Ability : ScriptableObject
    {
        [Header("General config")]
        [SerializeField] Sprite Icon;
        [SerializeField] int cost;
        [SerializeField] float cooldown;
        [SerializeField] bool isLearned;

        protected IAbilities behavior;

        abstract public void AttachComponentTo(GameObject obj);

        public void Use(AbilityUseParams useParams)
        {
            behavior.Use(useParams);
        }

        public int GetCost()
        {
            return cost;
        }
        public Sprite GetIcon()
        {
            return Icon;
        }
        public bool GetIsLearned()
        {
            return isLearned;
        }
    }
    public interface IAbilities
    {
        void Use(AbilityUseParams useParams);
    }
} 