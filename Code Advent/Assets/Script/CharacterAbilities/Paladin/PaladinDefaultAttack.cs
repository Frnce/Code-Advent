using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent.Player
{
    public class PaladinDefaultAttack : MonoBehaviour
    {
        PlayerController player;
        private void Awake()
        {
            player = GetComponent<PlayerController>();
        }
        public void Attack()
        {
            StartCoroutine(AttackCoroutine());
        }
        IEnumerator AttackCoroutine()
        {
            player.canMove = false;
            player.anim.SetTrigger("Attack_1");
            yield return new WaitForSeconds(0.25f);

            player.canMove = true;
        }
    }
}