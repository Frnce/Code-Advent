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
            icon.sprite = ability.getIcon();
            icon.enabled = true;
        }

        public void LearnAbility()
        {
            if (abilitySystem.GetAllLearnedAbility().Contains(abilitySystem.GetAllAbilities()[thisIndex]))
            {

            }
            abilitySystem.GetAllLearnedAbility().Add(abilitySystem.GetAllAbilities()[thisIndex]);
            Debug.Log("Ability learned : " + abilitySystem.GetAllLearnedAbility()[thisIndex].name);
        }
    }
}