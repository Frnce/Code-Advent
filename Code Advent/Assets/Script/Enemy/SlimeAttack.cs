using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent.Enemies
{
    public class SlimeAttack : GoapAction
    {
        private bool attacked = false;

        public SlimeAttack()
        {
            AddEffect("damagePlayer", true);
            cost = 100f;
        }
        public override bool CheckProceduralPrecondition(GameObject agent)
        {
            target = GameObject.Find("Player");
            return target != null;
        }

        public override bool IsDone()
        {
            return attacked;
        }

        public override bool Perform(GameObject agent)
        {
            Slime currentSlime= agent.GetComponent<Slime>();
            if (currentSlime.stamina >= (cost))
            {

//                currentSlime.animator.Play("wolfAttack");

                int damage = currentSlime.attack.GetValue();
                //if (currentSlime.player.isBlocking)
                //{
                //    damage -= currentSlime.player.defense;
                //}

                currentSlime.player.playerStats.currentHealth -= damage;
                currentSlime.stamina -= cost;

                attacked = true;
                return true;
            }
            else
            {
                return false;
            }
        }

        public override bool requiresInRange()
        {
            return true;
        }

        public override void _Reset()
        {
            attacked = false;
            target = null;
        }
    }

}