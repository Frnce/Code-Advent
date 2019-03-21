﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Advent.Entities;
using Advent.Inventories;
using Advent.Items;

namespace Advent
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
        Vector3 movement;
        Rigidbody2D rb2d;
        Animator anim;
        bool isMoving;
        bool canMove = true;
        bool canAttack = true;
        bool isAttack = false;
        bool isXAxisUsed = false;
        bool isYAxisUsed = false;

        [HideInInspector]
        public bool onMenu = false;

        StatSystem statSystem;
        // Start is called before the first frame update
        public void Start()
        {
            statSystem = StatSystem.instance;
            rb2d = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
        }
        private void Update()
        {
            if (!onMenu)
            {
                GetInput();
            }
            SetMovementAnimation();
            if (isAttack)
            {
                if (canAttack)
                {
                    StartCoroutine(CourAttack());
                }
            }
        }
        IEnumerator CourAttack()
        {
            canMove = false;
            canAttack = false;
            anim.SetTrigger("defaultAttackTrigger");
            yield return new WaitForSeconds(0.483f); // inline with attack animation length
            anim.ResetTrigger("defaultAttackTrigger");
            canMove = true;
            canAttack = true;
        }
        public void GetInput()
        {
            movement = Vector3.down;
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
            isAttack = Input.GetKeyDown(KeyCode.K);
        }
        private void FixedUpdate()
        {
            if (canMove)
            {
                if (movement != Vector3.zero)
                {
                    isMoving = true;
                    Move();
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
            rb2d.MovePosition(transform.position + movement * statSystem.speed.GetValue()* Time.deltaTime);
        }
        void SetMovementAnimation()
        {
            if (Input.GetAxisRaw("Horizontal") != 0)
            {
                isXAxisUsed = true;
            }
            else
            {
                isXAxisUsed = false;
            }

            if(Input.GetAxisRaw("Vertical") != 0)
            {
                isYAxisUsed = true;
            }
            else
            {
                isYAxisUsed = false;
            }

            if (isXAxisUsed && !isYAxisUsed)
            {
                anim.SetFloat("xMove", movement.x);
                anim.SetFloat("yMove", 0f);
            }
            if (isYAxisUsed && !isXAxisUsed)
            {
                anim.SetFloat("xMove", 0f);
                anim.SetFloat("yMove", movement.y);
            }
        }
    }
}