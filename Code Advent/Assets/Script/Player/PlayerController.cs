using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent.Player
{
    public class PlayerController : MonoBehaviour
    {
        [HideInInspector]
        public PlayerStats playerStats;

        public Transform weapon; //Change to scriptable objects ?
        Vector3 worldPoint;
        float xDir;
        float yDir;

        Animator anim;
        Rigidbody2D rb2d;

        bool canMove = true;
        bool isMoving = false;
        bool isFacingRight;
        private void Awake()
        {
            anim = GetComponent<Animator>();
            rb2d = GetComponent<Rigidbody2D>();
            playerStats = FindObjectOfType<PlayerStats>();
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
            Aim();
            //Attack();
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
        void Aim()
        {
            Vector2 mouse = Camera.main.ScreenToViewportPoint(Input.mousePosition);        //Mouse position
            Vector3 objpos = Camera.main.WorldToViewportPoint(weapon.position);        //Object position on screen
            Vector2 relobjpos = new Vector2(objpos.x - 0.5f, objpos.y - 0.5f);            //Set coordinates relative to object
            Vector2 relmousepos = new Vector2(mouse.x - 0.5f, mouse.y - 0.5f) - relobjpos;
            float angle = Vector2.Angle(Vector2.up, relmousepos);    //Angle calculation
            if (relmousepos.x > 0)
                angle = 360 - angle;
            Quaternion quat = Quaternion.identity;
            quat.eulerAngles = new Vector3(0, 0, angle); //Changing angle
            weapon.rotation = quat;
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
    }
}