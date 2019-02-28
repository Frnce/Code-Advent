using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent.Player
{
    public class PaladinHammerThrow : MonoBehaviour
    {
        public GameObject projectile;
        public float maxDistance;
        public float projectileSpeed;
        public int baseDamage;
        PlayerController player;

        Vector3 mousePos, mouseVector;
        // Start is called before the first frame update
        void Start()
        {
            player = PlayerController.instance;
        }

        // Update is called once per frame
        void Update()
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            mouseVector = (mousePos - transform.position).normalized;
        }

        public void Throw()
        {
            PaladinHammer hammer = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<PaladinHammer>();
            hammer.SetupMovement(mouseVector,projectileSpeed,maxDistance);
            hammer.SetupDamage(baseDamage);
        }
    }

}