using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent.Player
{
    public class PlayerController : MonoBehaviour
    {
        Vector3 worldPoint;
        float movementSpeed = 5f;

        float xDir;
        float yDir;

        Animator anim;
        Rigidbody2D rb2d;
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
        }
        private void FixedUpdate()
        {
            Movement();
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
        }
        void Movement()
        {
            rb2d.velocity = new Vector2(Mathf.Lerp(0, xDir * movementSpeed, 0.8f),
                                               Mathf.Lerp(0, yDir * movementSpeed, 0.8f));
        }
    }
}