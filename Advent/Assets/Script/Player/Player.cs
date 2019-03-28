using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent.Entities
{
    public class Player : EntityObject
    {
        public static Player instance;
        private void Awake()
        {
            instance = this;
        }
        GameManager gameManager;
        private bool isMoving = false;
        private Animator anim;
        protected override void Start()
        {
            gameManager = GameManager.instance;
            anim = GetComponent<Animator>();
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
                isMoving = true;
            }
            else
            {
                isMoving = false;
            }
            anim.SetBool("isMoving", isMoving);
        }
        protected override void AttemptMove<T>(int xDir, int yDir)
        {
            base.AttemptMove<T>(xDir, yDir);
            RaycastHit2D hit;
            CheckIfGameOver();
            anim.SetFloat("xMove", xDir);
            anim.SetFloat("yMove", yDir);
            if (Move(xDir, yDir, out hit))
            {
                //Call RandomizeSfx of SoundManager to play the move sound, passing in two audio clips to choose from.
            }
            gameManager.playersTurn = false;
        }
        protected override void OnCantMove<T>(T component)
        {
            isMoving = false;
            Enemy hitEnemy = component as Enemy;
            hitEnemy.DamageEntity(hitEnemy.name,attack.GetValue());
        }   
        private void CheckIfGameOver()
        {
            if(currentHealth <= 0)
            {
                gameManager.GameOver();
            }
        }
        public override void Die()
        {
            base.Die();
            gameObject.SetActive(false);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Exit"))
            {
                Debug.Log("Exit to next floor"); //TODO Add function on Exit
            }
        }
    }
}