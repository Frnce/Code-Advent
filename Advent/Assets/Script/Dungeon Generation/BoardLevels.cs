using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent.Dungeons
{
    [CreateAssetMenu(fileName = "New Board Level", menuName = "Dungeon/board Level")]
    public class BoardLevels : ScriptableObject
    {
        new public string name;
        public BoardParameters[] boardParameters = null;
    }
}