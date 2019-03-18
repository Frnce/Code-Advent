using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Advent.Enemies;

namespace Advent
{
    public class RegenAction : GOAPAction
    {

        private bool regened = false;

        public RegenAction()
        {
            AddEffect("stayAlive", true);
            cost = 500f;
        }

        public override void _Reset()
        {
            regened = false;
        }

        public override bool IsDone()
        {
            return regened;
        }

        public override bool requiresInRange()
        {
            return true;
        }

        public override bool CheckProceduralPrecondition(GameObject agent)
        {
            target = GameObject.Find("Player");
            return target != null;
        }

        public override bool Perform(GameObject agent)
        {
            MeleeEnemy e = agent.GetComponent<MeleeEnemy>();
            e.PassiveRegen();
            regened = true;
            return true;
        }
    }

}