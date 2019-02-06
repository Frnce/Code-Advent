using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        public float movementSpeed;

        Rigidbody2D rb2d;

        float xDir;
        float yDir;
        private void Awake()
        {
            rb2d = GetComponent<Rigidbody2D>();
        }
        // Start is called before the first frame update
        void Start()
        {
            GetInput();

            //incorporate Stats on movements;
        }

        // Update is called once per frame
        void Update()
        {
            GetInput();
        }
        private void FixedUpdate()
        {
            PlayerMovement();
        }

        private void PlayerMovement()
        {
            rb2d.velocity = new Vector2(Mathf.Lerp(0, xDir * movementSpeed, 0.8f), Mathf.Lerp(0, yDir * movementSpeed, 0.8f));
        }

        void GetInput()
        {
            xDir = Input.GetAxisRaw("Horizontal");
            yDir = Input.GetAxisRaw("Vertical");
        }
    }

}