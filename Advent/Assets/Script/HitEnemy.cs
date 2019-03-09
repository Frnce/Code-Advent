using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Advent.Enemies;
using Advent.Player;

namespace Advent
{
    public class HitEnemy : MonoBehaviour
    {
        PlayerController player;
        private void Start()
        {
            player = PlayerController.instance;
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Enemy"))
            {
                other.GetComponent<Enemy>().TakeDamage(player.physicalAttack.GetValue());
            }
        }
    }
}
