using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Advent.Enemy
{
    public class MeleeTypeEnemy : Enemy
    {
        Rigidbody2D rb2d;

        public GameObject target;
        // Start is called before the first frame update
        public override void Start()
        {
            base.Start();
            rb2d = GetComponent<Rigidbody2D>();
        }
        private void FixedUpdate()
        {
            Movement();
        }
        public override void Movement()
        {
            // base.Movement();
            rb2d.MovePosition(Vector2.MoveTowards(transform.position, target.transform.position, speed.GetValue() * Time.deltaTime));
        }
    }

}