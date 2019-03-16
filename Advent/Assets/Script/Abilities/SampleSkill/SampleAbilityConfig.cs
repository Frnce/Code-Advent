using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Advent.Enemies;

namespace Advent.Abilities
{
    public struct AbilityUseParams
    {
        public float baseDamage;

        public AbilityUseParams(float baseDamage)
        {
            this.baseDamage = baseDamage;
        }
    }
    [CreateAssetMenu(menuName = "Ability/Sample")]
    public class SampleAbilityConfig : Ability
    {
        [Header("Sample Ability Specific")]
        [SerializeField] int extraDamage = 10;


        public override void AttachComponentTo(GameObject obj)
        {
            var behaviorComponent = obj.AddComponent<SampleAbilityBehavior>();
            behaviorComponent.SetConfig(this);
            behavior = behaviorComponent;
        }
        public float GetExtraDamage()
        {
            return extraDamage;
        }
    }

}