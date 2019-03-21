using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Advent.Stats;

namespace Advent.Entities
{
    [CreateAssetMenu(menuName = "Entity")]
    public class Entity : ScriptableObject
    {
        new public string name = "New Character";

        public int baseStr;
        public int baseDex;
        public int baseInt;
        public int baseVit;

        public int expGiven; //For enemies only else , leave it to zero
    }
}