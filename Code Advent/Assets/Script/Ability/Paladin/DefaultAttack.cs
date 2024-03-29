﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Advent.Player;

namespace Advent.Abilities
{
    [CreateAssetMenu(menuName = "Abilities/Paladin/DefaultAttack")]
    public class DefaultAttack : Ability
    {
        private PaladinDefaultAttack paladinDefault;
        public override void Initialize(GameObject obj)
        {
            paladinDefault = obj.GetComponent<PaladinDefaultAttack>();
        }
        public override void TriggerAbility()
        {
            paladinDefault.Attack();
        }
    }
}