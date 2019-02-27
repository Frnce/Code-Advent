using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Advent.Player;
namespace Advent.Abilities
{
    [CreateAssetMenu(menuName = "Abilities/Paladin/Hammer Throw")]
    public class HammerThrowAttack : Ability
    {
        private PaladinHammerThrow hammerthrow;
        public override void Initialize(GameObject obj)
        {
            hammerthrow = obj.GetComponent<PaladinHammerThrow>();
        }

        public override void TriggerAbility()
        {
            hammerthrow.Throw();
        }
    }
}