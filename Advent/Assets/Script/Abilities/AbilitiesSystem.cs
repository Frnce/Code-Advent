using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Advent.Enemies;
using Advent.Stats;

namespace Advent.Abilities
{
    public class AbilitiesSystem : MonoBehaviour
    {
        [SerializeField] List<Ability> allAbilities = new List<Ability>(); // list of all the available ability a certain class can used
        [SerializeField] List<Ability> learnedAbilities = new List<Ability>(); // Learned skills/Abilities of the character and this are all that can be used currently of the player
        [SerializeField] List<Ability> activeAbilities = new List<Ability>(); // SKills that are active and can be used
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
            if (activeAbilities.Count > 0)
            {
                activeAbilities[abilityIndex].Use(abilityParams);
            }
            else
            {
                Debug.Log("No Ability Learned / Not yet learned");
            }
        }

        public List<Ability> GetAllAbilities()
        {
            return allAbilities;
        }
        public List<Ability> GetAllLearnedAbility()
        {
            return learnedAbilities;
        }
        public List<Ability> GetActiveAbilities()
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