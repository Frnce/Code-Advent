using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Advent.Stats;

namespace Advent.Character
{
    [CreateAssetMenu(fileName = "New Character", menuName = "Entity/Character")]
    public class CharacterClass : ScriptableObject
    {
        new public string name = "New Character";

        public int baseStr;
        public int baseDex;
        public int baseInt;
        public int baseVit;
        public int baseSpeed;
    }
}