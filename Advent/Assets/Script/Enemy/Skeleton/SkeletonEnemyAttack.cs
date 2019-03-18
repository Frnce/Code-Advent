using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Advent.Player;
namespace Advent.Enemies
{
    public class SkeletonEnemyAttack : GOAPAction
    {
        private bool attacked = false;

        public SkeletonEnemyAttack()
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
            SkeletonEnemy currentEntity = agent.GetComponent<SkeletonEnemy>();
            if (currentEntity.stamina >= (cost))
            {
                int damage = currentEntity.physicalAttack.GetValue();
                currentEntity.statSystem.TakeDamage(damage);
                currentEntity.stamina -= cost;

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