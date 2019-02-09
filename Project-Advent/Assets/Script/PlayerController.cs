using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SA.models;

namespace SA.player
{
    public class PlayerController : MonoBehaviour
    {
        public float movementSpeed;

        public EntityStats stats;

        Animator anim;

        float xDir;
        float yDir;
        bool cast;
        private void Awake()
        {
            anim = GetComponent<Animator>();
        }
        private void Start()
        {
            GetInput();
        }
        private void Update()
        {
            GetInput();
            Attack();
        }
        void Attack()
        {
            if (cast)
            {
                anim.SetTrigger("cast");
            }
        }
        public Vector2 GetInputDirection()
        {
            xDir = Input.GetAxisRaw("Horizontal");
            yDir = Input.GetAxisRaw("Vertical");

            return new Vector2(xDir, yDir);
        }
        public void GetInput()
        {
            cast = Input.GetKeyDown(KeyCode.K);
        }
    }
}