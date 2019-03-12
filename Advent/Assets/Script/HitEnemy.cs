﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Advent.Enemies;
using Advent.Stats;

namespace Advent
{
    public class HitEnemy : MonoBehaviour
    {
        StatSystem statSystem;
        private void Start()
        {
            statSystem = StatSystem.instance;
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Enemy"))
            {
                other.GetComponent<Enemy>().TakeDamage(statSystem.physicalAttack.GetValue());
            }
        }
    }
}