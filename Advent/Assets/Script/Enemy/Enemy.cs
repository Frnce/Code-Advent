using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Advent.Loot;

namespace Advent.Entities
{
    public class Enemy : EntityObject
    {
        private Transform target;
        private LootScript lootDrop;
        //private bool skipMove;
        // Start is called before the first frame update
        protected override void Start()
        {
            GameManager.instance.AddEnemyToList(this);
            target = GameObject.FindGameObjectWithTag("Player").transform;
            lootDrop = GetComponent<LootScript>();
            base.Start();
        }

        // Update is called once per frame
        void Update()
        {

        }
        protected override void AttemptMove<T>(int xDir, int yDir)
        {
            base.AttemptMove<T>(xDir, yDir);
        }
        public void MoveEnemy()
        {
            int xDir = 0;
            int yDir = 0;

            if (Mathf.Abs(target.position.x - transform.position.x) < float.Epsilon)
            {
                yDir = target.position.y > transform.position.y ? 1 : -1;
            }
            else
            {
                xDir = target.position.x > transform.position.x ? 1 : -1;
            }
            AttemptMove<Player>(xDir, yDir);
        }
        protected override void OnCantMove<T>(T component)
        {
            Player hitPlayer = component as Player;

            hitPlayer.DamageEntity(hitPlayer.name,attack.GetValue());
            //hitPlayer.LoseFood(playerDamage);

            ////Set the attack trigger of animator to trigger Enemy attack animation.
            //animator.SetTrigger("enemyAttack");
        }
        public override void Die()
        {
            base.Die();
            lootDrop.DropLoot();
            levelSystem.GainExp(entity.expGiven);
            GameManager.instance.RemoveEnemyToList(this);
            Destroy(gameObject);
        }
    }
}