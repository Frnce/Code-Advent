using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent.Player
{
    public class PlayerController : MonoBehaviour
    {

        public float movementSpeed = 5f;
        public float startTimeBetweenAttack = 0.5f;
        float timeBetweenAttack;
        Vector3 worldPoint;
        float xDir;
        float yDir;
        bool attackInput;

        Animator anim;
        Rigidbody2D rb2d;

        bool canMove = true;
        private void Awake()
        {
            anim = GetComponent<Animator>();
            rb2d = GetComponent<Rigidbody2D>();
        }
        // Start is called before the first frame update
        void Start()
        {
            GetPlayerInput();
            timeBetweenAttack = startTimeBetweenAttack;
        }

        // Update is called once per frame
        void Update()
        {
            GetMousePosition();
            GetPlayerInput();

            Attack();
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
            attackInput = Input.GetMouseButton(0);
        }
        void Movement()
        {
            rb2d.velocity = new Vector2(Mathf.Lerp(0, xDir * movementSpeed, 0.8f),
                                               Mathf.Lerp(0, yDir * movementSpeed, 0.8f));
        }
        void Attack()
        {
            if (timeBetweenAttack <= 0)
            {
                if (attackInput)
                {
                    StartCoroutine(AttackCoroutine());
                    timeBetweenAttack = startTimeBetweenAttack;
                }
            }
            else
            {
                timeBetweenAttack -= Time.deltaTime;
            }

        }
        IEnumerator AttackCoroutine()
        {
            canMove = false;
            anim.SetTrigger("Attack_1");

            yield return new WaitForSeconds(startTimeBetweenAttack);

            canMove = true;
        }
    }
}