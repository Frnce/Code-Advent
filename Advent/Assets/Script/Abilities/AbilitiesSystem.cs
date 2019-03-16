using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Advent.Enemies;
using Advent.Stats;

namespace Advent.Abilities
{
    public class AbilitiesSystem : MonoBehaviour
    {
        [SerializeField] SampleAbilityConfig[] abilities;
        StatSystem statSystem;
        // Start is called before the first frame update
        void Start()
        {
            statSystem = StatSystem.instance;
            abilities[0].AttachComponentTo(gameObject);
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                AttemptAbility(0);
            }
        }

        private void AttemptAbility(int abilityIndex)
        {
            //TODO Implement Energy usage
            //TODO Check if Energy is enough
            var abilityParams = new AbilityUseParams(statSystem.physicalAttack.GetValue());
            abilities[abilityIndex].Use(abilityParams);
        }
    }
}