using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Advent.Dungeons
{
    [CreateAssetMenu(fileName = "New Board", menuName = "Dungeon/board parameters")]
    public class BoardParameters : ScriptableObject
    {
        public EnemySpawns[] enemySpawns = null;
        public IntRange chestCount = new IntRange(3, 5);
        public Tile[] groundTile = null;
        public Tile[] pitTile = null;
        public Tile[] topWallTile = null;
        public Tile[] botWallTile = null;
        public IntRange deviationRate = new IntRange(10,15);
        public IntRange roomRate = new IntRange(10,15);
        public IntRange maxRouteLength = new IntRange(100, 150);
        public IntRange maxRoutes = new IntRange(20, 25);
        public GameObject[] chests;
    }
    [System.Serializable]
    public class EnemySpawns
    {
        public GameObject enemyObject;
        public IntRange count;

        public EnemySpawns(GameObject gameObject,IntRange range)
        {
            enemyObject = gameObject;
            count = range;
        }
    }
}