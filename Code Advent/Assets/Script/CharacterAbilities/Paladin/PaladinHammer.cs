using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Advent.Player;

namespace Advent
{
    public class PaladinHammer : MonoBehaviour
    {
        public float rotateSpeed = 1000f;
        float speed;
        float maxDistance;
        Vector3 direction;

        Rigidbody2D rb2d;
        PlayerController player;
        public void SetupMovement(Vector3 _direction, float _speed,float _maxDistance)
        {
            direction = _direction;
            speed = _speed;
            maxDistance = _maxDistance;
        }
        // Start is called before the first frame update
        void Start()
        {
            player = PlayerController.instance;
            rb2d = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update()
        {
            RotateHammer();
            Move();
            DestroyIfCheckMaxDistance();
        }
        void RotateHammer()
        {
            transform.Rotate(0, 0, -rotateSpeed * Time.deltaTime);
        }
        void Move()
        {
            Vector3 tempPos = transform.position; //capture current position
            tempPos += direction * speed * Time.deltaTime; //find new position
            transform.position = tempPos;
        }
        void DestroyIfCheckMaxDistance()
        {
            if(Vector2.Distance(player.transform.position,transform.position) > maxDistance)
            {
                Destroy(gameObject);
            }
        }
        //add damage Dealing ;
    }
}