using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent.Player
{
    public class PaladinHammerThrow : MonoBehaviour
    {
        public GameObject projectile;
        public float projectileSpeed;
        PlayerController player;

        Vector3 mousePos, mouseVector;
        private void Awake()
        {
            player = PlayerController.instance;
        }
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseVector = (mousePos - transform.position).normalized;
        }

        public void Throw()
        {
            PaladinHammer hammer = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<PaladinHammer>();
            hammer.Setup(mouseVector,projectileSpeed);
        }
    }

}