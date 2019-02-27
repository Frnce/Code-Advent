using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent
{
    public class PaladinHammer : MonoBehaviour
    {
        public float rotateSpeed = 1000f;
        float speed;
        Vector3 direction;

        Rigidbody2D rb2d;
        private void Awake()
        {
            rb2d = GetComponent<Rigidbody2D>();
        }
        public void Setup(Vector3 _direction, float _speed)
        {
            direction = _direction;
            speed = _speed;
        }
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            RotateHammer();
            Destroy(gameObject, 3f); //Temporary
        }
        private void FixedUpdate()
        {
            Move();
        }
        void RotateHammer()
        {
            transform.Rotate(0, 0, -rotateSpeed * Time.deltaTime);
        }
        void Move()
        {
            Vector3 tempPos = transform.position; //capture current position
            tempPos += direction * speed * Time.fixedDeltaTime; //find new position
            rb2d.velocity = tempPos; //update position
        }
    }
}