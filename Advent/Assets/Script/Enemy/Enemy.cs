using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Advent.Stats;

namespace Advent.Enemy
{
    public abstract class Enemy : CharacterStats
    {
        public virtual void Start()
        {
            InitStats();
        }
        public virtual void Movement()
        {
            Debug.Log("No Movement function made");
        }
        public virtual void Attack()
        {
            Debug.Log("No Attack function made");
        }
    }
}
