using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent.Player
{
    public class PlayerController : MonoBehaviour
    {
        public int HP = 10;
        public int MP = 10;
        public int attack = 5;
        public float movementSpeed = 5f;
        Vector3 worldPoint;
        float xDir;
        float yDir;

        Animator anim;
        Rigidbody2D rb2d;

        bool canMove = true;
        bool isMoving = false;
        bool isAlive = true;
        private void Awake()
        {
            anim = GetComponent<Animator>();
            rb2d = GetComponent<Rigidbody2D>();
        }
        // Start is called before the first frame update
        void Start()
        {
            GetPlayerInput();
        }

        // Update is called once per frame
        void Update()
        {
            GetMousePosition();
            GetPlayerInput();

            CheckPlayerDeath();

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
            worldPoint = CaptureMousePos();
            anim.SetFloat("xPosition", worldPoint.x);
            anim.SetFloat("yPosition", worldPoint.y);
        }
        void GetPlayerInput()
        {
            xDir = Input.GetAxisRaw("Horizontal");
            yDir = Input.GetAxisRaw("Vertical");
        }
        void Movement()
        {
            isMoving = true;
            rb2d.velocity = new Vector2(Mathf.Lerp(0, xDir * movementSpeed, 0.8f),
                                               Mathf.Lerp(0, yDir * movementSpeed, 0.8f));
        }
        private Vector3 CaptureMousePos()
        {
            Vector2 ret = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            ret *= 2;
            ret -= Vector2.one;
            float max = 0.9f;
            if (Mathf.Abs(ret.x) > max || Mathf.Abs(ret.y) > max)
            {
                ret = ret.normalized;
            }
            return ret;
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
        public void DamagePlayer(int dmg)
        {
            HP -= dmg;
            Debug.Log("player has been hit");
        }
        public void CheckPlayerDeath()
        {
            if (HP <= 0)
            {
                isAlive = false;
                Debug.Log("Dead");
            }
        }
    }
}