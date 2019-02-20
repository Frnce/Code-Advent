﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Class" , menuName = "Character/Class")]
public class CharacterClasses : ScriptableObject
{
    public Stat baseStr;
    public Stat baseDex;
    public Stat baseInt;
    public Stat baseCon;
}
