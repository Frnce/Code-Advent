using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA.player
{
    public class PlayerMovement : MonoBehaviour
    {
        PlayerController player;
        Rigidbody2D rb2d;
        Animator anim;

        bool isMoving = false;
        Vector2 lastMove;
        // Start is called before the first frame update

        private void Awake()
        {
            rb2d = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
        }
        void Start()
        {
            player = FindObjectOfType<PlayerController>();
        }

        // Update is called once per frame
        void Update()
        {
            player.GetInputDirection();
            SetAnimations();
        }
        private void FixedUpdate()
        {
            isMoving = false;
            Move();
            if (player.GetInputDirection().x > 0f || player.GetInputDirection().x < 0f || player.GetInputDirection().y > 0f || player.GetInputDirection().y < 0f)
            {
                isMoving = true;
                lastMove = new Vector2(player.GetInputDirection().x, player.GetInputDirection().y);
            }
        }
        void Move()
        {
            rb2d.velocity = new Vector2(Mathf.Lerp(0, player.GetInputDirection().x * player.movementSpeed, 0.8f), Mathf.Lerp(0, player.GetInputDirection().y * player.movementSpeed, 0.8f));
        }
        void SetAnimations()
        {
            anim.SetFloat("xMove", player.GetInputDirection().x);
            anim.SetFloat("yMove", player.GetInputDirection().y);
            anim.SetFloat("xLastMove", lastMove.x);
            anim.SetFloat("yLastMove", lastMove.y);
            anim.SetBool("isMoving", isMoving);
        }
    }
}