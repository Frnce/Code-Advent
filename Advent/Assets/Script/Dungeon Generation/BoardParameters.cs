using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent.Dungeons
{
    [CreateAssetMenu(fileName = "New Board", menuName = "Dungeonboard")]
    public class BoardParameters : ScriptableObject
    {
        public int columns = 100;
        public int rows = 100;
        public IntRange numOfRooms = new IntRange(15, 20);
        public IntRange roomWidth = new IntRange(3, 10);
        public IntRange roomHeight = new IntRange(3, 10);
        public IntRange corridorLength = new IntRange(6, 10);
        public IntRange enemyCount = new IntRange(0, 10);
        public GameObject[] floorTiles;
        public GameObject[] wallTiles;
        public GameObject[] outerWallTiles;
        public GameObject[] enemies;
    }
}