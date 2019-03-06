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
        float xMove, yMove;
        Rigidbody2D rb2d;
        // Start is called before the first frame update
        void Start()
        {
            rb2d = GetComponent<Rigidbody2D>();
            stats = GetComponent<PlayerStats>();
        }
        private void Update()
        {
            GetInput();
        }
        public void GetInput()
        {
            xMove = Input.GetAxisRaw("Horizontal");
            yMove = Input.GetAxisRaw("Vertical");
        }
        private void FixedUpdate()
        {
            rb2d.velocity = new Vector2(xMove * stats.speed.GetValue(), yMove * stats.speed.GetValue());
        }
    }
}