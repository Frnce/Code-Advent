using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Advent.Stats;

namespace Advent.Character
{
    [CreateAssetMenu(fileName = "New Character", menuName = "Entities")]
    public class CharacterClass : ScriptableObject
    {
        new public string name = "New Character";

        public int baseStr;
        public int baseDex;
        public int baseInt;
        public int baseVit;
        public int baseSpeed;

        public int expGiven; //For enemies only else , leave it to zero
    }
}