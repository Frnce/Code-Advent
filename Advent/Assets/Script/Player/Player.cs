using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent.Entities
{
    public class Player : EntityObject
    {
        GameManager gameManager;
        protected override void Start()
        {
            gameManager = GameManager.instance;
            base.Start();
        }
        private void OnDisable()
        {
            //gameManager.playerFoodPoints = food;
        }
        private void Update()
        {
            if (!gameManager.playersTurn)
            {
                return;
            }

            int horizontal = 0;
            int vertical = 0;

            horizontal = (int)Input.GetAxisRaw("Horizontal");
            vertical = (int)Input.GetAxisRaw("Vertical");

            if(horizontal != 0)
            {
                vertical = 0;
            }
            if(horizontal != 0 || vertical != 0)
            {
                AttemptMove<Enemy>(horizontal, vertical);
            }
        }
        protected override void AttemptMove<T>(int xDir, int yDir)
        {
            base.AttemptMove<T>(xDir, yDir);
            RaycastHit2D hit;
            CheckIfGameOver();
            if (Move(xDir, yDir, out hit))
            {
                //Call RandomizeSfx of SoundManager to play the move sound, passing in two audio clips to choose from.
            }
            gameManager.playersTurn = false;
        }
        protected override void OnCantMove<T>(T component)
        {
            Enemy hitEnemy = component as Enemy;
            hitEnemy.DamageEntity(1);
        }
        private void CheckIfGameOver()
        {
            if(CurrentHealth <= 0)
            {
                gameManager.GameOver();
            }
        }
        public override void Die()
        {
            base.Die();
            gameObject.SetActive(false);
        }
    }
}