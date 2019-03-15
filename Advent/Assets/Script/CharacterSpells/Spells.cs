using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent.Spells
{
    [System.Serializable]
    public class Spells
    {
        public int id;
        public string name;
        public string description;

        public Spells(Spells spells)
        {
            id = spells.id;
            name = spells.name;
            description = spells.description;
        }
    }
}