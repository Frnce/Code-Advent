using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Advent;
using Advent.CharacterStats;
using Advent.Items;

namespace Advent.Player
{
    public class PlayerController : MonoBehaviour
    {
        #region singleton
        public static PlayerController instance;
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(instance);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        #endregion
        [HideInInspector]
        public PlayerStats playerStats;

        public GameObject weapon; //Change to scriptable objects ?

        Vector3 worldPoint;
        float xDir;
        float yDir;

        Animator anim;
        Rigidbody2D rb2d;

        bool canMove = true;
        bool isMoving = false;
        bool isFacingRight;

        // Start is called before the first frame update
        void Start()
        {
            anim = GetComponent<Animator>();
            rb2d = GetComponent<Rigidbody2D>();
            playerStats = FindObjectOfType<PlayerStats>();

            GetPlayerInput();
        }

        // Update is called once per frame
        void Update()
        {
            GetPlayerInput();
            Aim();
            FlipCharacter();
        }
        private void FixedUpdate()
        {
            if (canMove)
            {
                Movement();
            }
            else
            {
                rb2d.velocity = Vector2.zero;
            }
            anim.SetBool("isMoving",isMoving);
        }
        void GetPlayerInput()
        {
            xDir = Input.GetAxisRaw("Horizontal");
            yDir = Input.GetAxisRaw("Vertical");
        }
        void Movement()
        {
            if(xDir != 0 || yDir != 0)
            {
                isMoving = true;
            }
            else
            {
                isMoving = false;
            }
            rb2d.velocity = new Vector2(Mathf.Lerp(0, xDir * playerStats.speed.GetValue(), 0.8f),
                                               Mathf.Lerp(0, yDir * playerStats.speed.GetValue(), 0.8f));
        }
        void FlipCharacter()
        {
            Vector2 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (mouse.x > transform.position.x && isFacingRight)
            {
                flip();
            }
            else if (mouse.x < transform.position.x && !isFacingRight)
            {
                flip();
            }
        }
        void flip()
        {
            isFacingRight = !isFacingRight;

            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
        void Aim()
        {
            //The Weapon should be facing up or vector2.up for it to work properly
            Vector2 mouse = Camera.main.ScreenToViewportPoint(Input.mousePosition);        //Mouse position
            Vector3 objpos = Camera.main.WorldToViewportPoint(weapon.transform.position);        //Object position on screen
            Vector2 relobjpos = new Vector2(objpos.x - 0.5f, objpos.y - 0.5f);            //Set coordinates relative to object
            Vector2 relmousepos = new Vector2(mouse.x - 0.5f, mouse.y - 0.5f) - relobjpos;
            float angle = Vector2.Angle(Vector2.up, relmousepos);    //Angle calculation
            if (relmousepos.x > 0)
                angle = 360 - angle;
            Quaternion quat = Quaternion.identity;
            quat.eulerAngles = new Vector3(0, 0, angle); //Changing angle
            weapon.transform.rotation = quat;
        }
        public void Attack()
        {
            StartCoroutine(AttackCoroutine());
        }
        IEnumerator AttackCoroutine()
        {
            canMove = false;
            anim.SetTrigger("Attack_1");

            yield return new WaitForSeconds(0.25f);

            canMove = true;
        }
    }
}