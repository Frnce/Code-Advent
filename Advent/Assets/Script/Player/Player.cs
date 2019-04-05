using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Advent.Utilities;

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
        public SelectorController selector;
        [SerializeField]
        private Tilemap gridViewer = null;
        GameManager gameManager;
        private bool isMoving = true;
        private bool isSelectionMode = false;
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
        private void Update()
        {
            if (!gameManager.playersTurn)
            {
                return;
            }

            if (Input.GetKeyDown(KeyCode.T) && isSelectionMode == false)
            {
                isSelectionMode = true;
                selector.SetIfActive(true);
                gridViewer.gameObject.SetActive(true);
                Debug.Log(isSelectionMode);
            }
            else if(Input.GetKeyDown(KeyCode.T) && isSelectionMode == true)
            {
                isSelectionMode = false;
                selector.SetIfActive(false);
                gridViewer.gameObject.SetActive(false);
                Debug.Log(isSelectionMode);
            }

            int horizontal = 0;
            int vertical = 0;
            int selectorHorizontal = 0;
            int selectorVertical = 0;

            if (!isSelectionMode)
            {
                horizontal = (int)Input.GetAxisRaw("Horizontal");
                vertical = (int)Input.GetAxisRaw("Vertical");
            }
            else
            {
                selectorHorizontal = (int)Input.GetAxisRaw("Horizontal");
                selectorVertical = (int)Input.GetAxisRaw("Vertical");
            }
            if (horizontal != 0 || vertical != 0)
            {
                AttemptMove<Enemy,ChestScript>(horizontal, vertical);
                //AttemptMove<ChestScript>(horizontal, vertical);
            }
            if(selectorHorizontal!= 0 || selectorVertical != 0)
            {
                selector.MoveSelector(selectorHorizontal, selectorVertical);
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
                isMoving = true;
                anim.SetTrigger("Move");
                gameManager.turns++;
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
        public bool GetIsSelectionMode(){return isSelectionMode;}
    }
}