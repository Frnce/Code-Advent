using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent.Abilities
{
    public class SampleAbilityBehavior : MonoBehaviour, IAbilities
    {
        SampleAbilityConfig config;

        public void SetConfig(SampleAbilityConfig configSet)
        {
            this.config = configSet;
        }
        public void Use(AbilityUseParams useParams)
        {
            print("Used Stamina : " + config.getCost());
            print("Sample Ability Used, Damage :" + config.GetExtraDamage());
            print("Player base damage :" + useParams.baseDamage);
        }

        // Start is called before the first frame update
        void Start()
        {
            print("SampleAbility Attached");
        }
    }

}