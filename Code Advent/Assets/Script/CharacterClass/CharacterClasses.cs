﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Advent.CharacterStat;

namespace Advent.CharacterClass
{
    [CreateAssetMenu(fileName = "New Class", menuName = "Character/Class")]
    public class CharacterClasses : ScriptableObject
    {
        public Stat baseStr;
        public Stat baseDex;
        public Stat baseVit;
        public Stat baseEne;
    }
}