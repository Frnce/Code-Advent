using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Advent.Player;

namespace Advent.Enemies
{
    public class MeleeTypeEnemyAttack : GOAPAction
    {
        private bool attacked = false;

        public MeleeTypeEnemyAttack()
        {
            AddEffect("damagePlayer", true);
            cost = 100f;
        }
        public override bool CheckProceduralPrecondition(GameObject agent)
        {
            target = PlayerController.instance.gameObject;
            return target != null;
        }

        public override bool IsDone()
        {
            return attacked;
        }

        public override bool Perform(GameObject agent)
        {
            MeleeTypeEnemy currentEnemy = agent.GetComponent<MeleeTypeEnemy>();
            if (currentEnemy.stamina >= (cost))
            {

                //                currentSlime.animator.Play("wolfAttack");

                int damage = currentEnemy.physicalAttack.GetValue();
                //if (currentSlime.player.isBlocking)
                //{
                //    damage -= currentSlime.player.defense;
                //}

                //currentSlime.player.playerStats.currentHealth -= damage;
                currentEnemy.player.TakeDamage(damage);
                currentEnemy.stamina -= cost;

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