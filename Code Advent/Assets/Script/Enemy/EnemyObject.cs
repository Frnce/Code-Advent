using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent.Enemies
{
    [CreateAssetMenu(menuName = "New Enemy")]
    public class EnemyObject : ScriptableObject
    {
        public float movementSpeed = 3f;
        public int health = 0;
        public int attack = 0;
        public int defense = 0;
    }
}