using System.Collections;
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

        protected IAbilities behavior;

        abstract public void AttachComponentTo(GameObject obj);

        public void Use(AbilityUseParams useParams)
        {
            behavior.Use(useParams);
        }

        public int getCost()
        {
            return cost;
        }
    }
    public interface IAbilities
    {
        void Use(AbilityUseParams useParams);
    }
} 