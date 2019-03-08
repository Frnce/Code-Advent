using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Advent.Stats;

namespace Advent.Player
{
    public class PlayerController : MonoBehaviour
    {
        #region singleton
        public static PlayerController instance;
        private void Awake()
        {
            instance = this;
        }
        #endregion
        PlayerStats stats;
        Vector3 movement;
        Rigidbody2D rb2d;
        Animator anim;
        bool isMoving;
        // Start is called before the first frame update
        void Start()
        {
            rb2d = GetComponent<Rigidbody2D>();
            stats = GetComponent<PlayerStats>();
            anim = GetComponent<Animator>();
        }
        private void Update()
        {
            GetInput();
        }
        public void GetInput()
        {
            movement = Vector3.zero;
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
        }
        private void FixedUpdate()
        {
            if (movement != Vector3.zero)
            {
                isMoving = true;
                Move();
                SetMovementAnimation();
            }
            else{
                isMoving = false;
            }
        }
        private void Move()
        {
            rb2d.MovePosition(transform.position + movement * stats.speed.GetValue() * Time.deltaTime);
        }
        void SetMovementAnimation()
        {
            anim.SetBool("isMoving",isMoving);
            anim.SetFloat("xMove",movement.x);
            anim.SetFloat("yMove",movement.y);
        }
    }
}