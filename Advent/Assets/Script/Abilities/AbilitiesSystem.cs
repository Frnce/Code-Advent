using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Advent.Enemies;
using Advent.Stats;

namespace Advent.Abilities
{
    public class AbilitiesSystem : MonoBehaviour
    {
        [SerializeField] Ability[] allAbilities; // list of all the available ability a certain class can used
        [SerializeField] Ability[] learnedAbilities; // Learned skills/Abilities of the character and this are all that can be used currently of the player
        [SerializeField] Ability[] activeAbilities; // SKills that are active and can be used
        StatSystem statSystem;

        public static AbilitiesSystem instance;
        private void Awake()
        {
            instance = this;
        }

        // Start is called before the first frame update
        void Start()
        {
            statSystem = StatSystem.instance;
            learnedAbilities = new Ability[allAbilities.Length];
        }
        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                AttemptAbility(1);
            }
            if (Input.GetKeyDown(KeyCode.U)) //Ability to hotkey
            {
                SetActiveAbility(1, 0);
            }
        }

        private void AttemptAbility(int abilityIndex)
        {
            //TODO Implement Energy usage
            //TODO Check if Energy is enough
            var abilityParams = new AbilityUseParams(statSystem.physicalAttack.GetValue());
            if (activeAbilities.Length > 0)
            {
                activeAbilities[abilityIndex].Use(abilityParams);
            }
            else
            {
                Debug.Log("No Ability Learned / Not yet learned");
            }
        }

        public Ability[] GetAllAbilities()
        {
            return allAbilities;
        }
        public Ability[] GetAllLearnedAbility()
        {
            return learnedAbilities;
        }
        public Ability[] GetActiveAbilities()
        {
            return activeAbilities;
        }
        public void SetActiveAbility(int keyIndex,int abilityIndex)
        {
            //KeyIndex = the one the user seted keys for abilities E.g alpha1-alpha0
            //AbilityIndex = the selected availableSkill to hotkeys
            activeAbilities[keyIndex] = learnedAbilities[abilityIndex];
            activeAbilities[keyIndex].AttachComponentTo(gameObject);
            Debug.Log("Ability Set to : " + keyIndex + " Key");
        }
    }
}