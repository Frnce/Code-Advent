using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Advent.Enemies
{
    public abstract class Enemy : ScriptableObject
    {
        public string enemyName = "New Enemy";

        public abstract void DamageEnemy(int dmg);
    }

}