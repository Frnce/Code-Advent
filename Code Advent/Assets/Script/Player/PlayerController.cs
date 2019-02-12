using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent.Player
{
    public class PlayerController : MonoBehaviour
    {

        public float movementSpeed = 5f;
        //public float startTimeBetweenAttack = 0.5f;
        //float timeBetweenAttack;
        Vector3 worldPoint;
        float xDir;
        float yDir;
        //bool attackInput;

        Animator anim;
        Rigidbody2D rb2d;

        bool canMove = true;
        bool isMoving = false;
        private void Awake()
        {
            anim = GetComponent<Animator>();
            rb2d = GetComponent<Rigidbody2D>();
        }
        // Start is called before the first frame update
        void Start()
        {
            GetPlayerInput();
            //timeBetweenAttack = startTimeBetweenAttack;
        }

        // Update is called once per frame
        void Update()
        {
            GetMousePosition();
            GetPlayerInput();

            //Attack();
        }
        private void FixedUpdate()
        {
            isMoving = false;
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
        void GetMousePosition()
        {
            worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            anim.SetFloat("xPosition", worldPoint.x);
            anim.SetFloat("yPosition", worldPoint.y);
        }
        void GetPlayerInput()
        {
            xDir = Input.GetAxisRaw("Horizontal");
            yDir = Input.GetAxisRaw("Vertical");
            //attackInput = Input.GetMouseButton(0);
        }
        void Movement()
        {
            isMoving = true;
            rb2d.velocity = new Vector2(Mathf.Lerp(0, xDir * movementSpeed, 0.8f),
                                               Mathf.Lerp(0, yDir * movementSpeed, 0.8f));
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