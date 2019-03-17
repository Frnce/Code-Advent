using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Advent.Abilities;

namespace Advent.UI
{
    public class SkillSlot : MonoBehaviour
    {
        public Ability ability;
        public Image icon;
        public int thisIndex;
        AbilitiesSystem abilitySystem;
        private void Start()
        {
            abilitySystem = AbilitiesSystem.instance;
            icon.sprite = ability.GetIcon();
            icon.enabled = true;
        }

        public void LearnAbility()
        {
            //Show Sub menu. 
            //Learn
            //Add to key
            abilitySystem.GetAllLearnedAbility()[thisIndex] = abilitySystem.GetAllAbilities()[thisIndex];
            Debug.Log("Ability learned : " + abilitySystem.GetAllLearnedAbility()[thisIndex].name);
        }
    }
}