using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Advent.Entities
{
    public class Player : EntityObject
    {
        public static Player instance;
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
            DontDestroyOnLoad(gameObject);
        }
        GameManager gameManager;
        private bool isMoving = false;
        private Animator anim;
        private int availablePoints = 0;
        private bool isOnDoor = false;
        private bool isFacingRight = false;

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

            if (horizontal != 0)
            {
                vertical = 0;
            }
            if (horizontal != 0 || vertical != 0)
            {
                AttemptMove<Enemy,ChestScript>(horizontal, vertical);
                //AttemptMove<ChestScript>(horizontal, vertical);
            }
            if(horizontal > 0 && !isFacingRight)
            {
                Flip();
            }
            else if(horizontal < 0 && isFacingRight)
            {
                Flip();
            }
            else
            {
                isMoving = false;
            }

            if (isOnDoor)
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    isOnDoor = false;
                    gameManager.GoToNextLevel();
                }
            }
        }

        public int GetAvailablePoints()
        {
            return availablePoints;
        }
        public void AddAvailablePoints(int additionalPoints)
        {
            availablePoints += additionalPoints;
        }
        public void UseAvailablePoint()
        {
            availablePoints--;
        }
        protected override void AttemptMove<Enemy,Chest>(int xDir, int yDir)
        {
            base.AttemptMove<Enemy,Chest>(xDir, yDir);
            CheckIfGameOver();
            anim.SetFloat("xMove", xDir);
            anim.SetFloat("yMove", yDir);
            if (canMove)
            {
                anim.SetTrigger("Move");
                //Call RandomizeSfx of SoundManager to play the move sound, passing in two audio clips to choose from.
            }
            gameManager.playersTurn = false;
        }
        protected override void OnCantMove<T>(T component)
        {
            isMoving = false;
            Enemy hitEnemy = component as Enemy;
            ChestScript chest = component as ChestScript;
            anim.SetTrigger("Attack");
            if (component == hitEnemy)
            {
                hitEnemy.DamageEntity(hitEnemy.name, attack.GetValue());
            }
            if(component == chest)
            {
                chest.OpenChest();
            }
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
        private void Flip()
        {
            isFacingRight = !isFacingRight;

            //GetChild 0 is the sprite renderer Gameobject
            Vector3 theScale = transform.GetChild(0).localScale;
            theScale.x *= -1;
            transform.GetChild(0).localScale = theScale;
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Exit"))
            {
                Debug.Log("On Door");
                isOnDoor = true;
            }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            isOnDoor = false;  
        }
    }
}