﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Advent.Stats;

namespace Advent.Player
{
    public class PlayerController : CharacterStats
    {
        #region singleton
        public static PlayerController instance;
        private void Awake()
        {
            instance = this;
        }
        #endregion
        Vector3 movement;
        Rigidbody2D rb2d;
        Animator anim;
        bool isMoving;
        bool canMove = true;

        // Start is called before the first frame update
        public void Start()
        {
            rb2d = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();

            InitStats();
        }
        private void Update()
        {
            GetInput();

            if (Input.GetKeyDown(KeyCode.K))
            {
                StartCoroutine(CourAttack());
            }
        }
        IEnumerator CourAttack()
        {
            canMove = false;
            anim.SetTrigger("attack1");
            yield return new WaitForSeconds(0.25f);

            canMove = true;
        }
        public void GetInput()
        {
            movement = Vector3.zero;
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
        }
        private void FixedUpdate()
        {
            if (canMove)
            {
                if (movement != Vector3.zero)
                {
                    isMoving = true;
                    Move();
                    SetMovementAnimation();
                }
                else
                {
                    isMoving = false;
                }
            }
            else
            {
                rb2d.MovePosition(transform.position);
            }
            anim.SetBool("isMoving", isMoving);
        }
        private void Move()
        {
            rb2d.MovePosition(transform.position + movement * speed.GetValue() * Time.deltaTime);
        }
        void SetMovementAnimation()
        {
            anim.SetFloat("xMove", movement.x);
            anim.SetFloat("yMove", movement.y);
        }
    }
}