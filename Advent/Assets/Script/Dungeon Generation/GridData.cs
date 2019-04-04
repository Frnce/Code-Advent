using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Advent.Entities;
using UnityEngine.Tilemaps;
using Advent.Utilities;

namespace Advent.Dungeons
{
    public class GridData : MonoBehaviour
    {
        public Tilemap tilemap;
        private void Start()
        {
            tilemap = GetComponent<Tilemap>();
        }
        private void OnEnable()
        {
            SetTileStatus();
        }
        private void OnDisable()
        {
            ResetTileStatus();
        }
        public void ResetTileStatus()
        {
            foreach (KeyValuePair<Vector3, WorldTile> item in GameTiles.instance.tiles)
            {
                item.Value.tileStatus = TileStatus.EMPTY;
            }
        }
        public void SetTileStatus()
        {
            Vector3 playerPoint = Player.instance.transform.position;
            List<Vector3Int> enemyPoints = new List<Vector3Int>();
            var playerWorldPoint = new Vector3Int(Mathf.FloorToInt(playerPoint.x), Mathf.FloorToInt(playerPoint.y), 0);
            foreach (KeyValuePair<Vector3, WorldTile> item in GameTiles.instance.tiles)
            {
                for (int i = 0; i < GameManager.instance.GetEnemyList().Count; i++)
                {
                    enemyPoints.Add(new Vector3Int(Mathf.FloorToInt(GameManager.instance.GetEnemyList()[i].transform.position.x), Mathf.FloorToInt(GameManager.instance.GetEnemyList()[i].transform.position.y), 0));
                }
                for (int a = 0; a < enemyPoints.Count; a++)
                {
                    if (enemyPoints[a] == item.Value.LocalPlace && playerWorldPoint != item.Value.LocalPlace)
                    {
                        item.Value.tileStatus = TileStatus.ENEMY;
                    }

                    else if (playerWorldPoint == item.Value.LocalPlace && enemyPoints[a] != item.Value.LocalPlace)
                    {
                        item.Value.tileStatus = TileStatus.PLAYER;
                    }
                }
            }
            SeekTileStatus();
        }
        public void SeekTileStatus()
        {
            foreach (KeyValuePair<Vector3, WorldTile> item in GameTiles.instance.tiles)
            {
                tilemap.SetTileFlags(item.Value.LocalPlace, TileFlags.None);
                switch (item.Value.tileStatus)
                {
                    case TileStatus.EMPTY:
                        tilemap.SetColor(item.Value.LocalPlace, Color.white);
                        break;
                    case TileStatus.PLAYER:
                        tilemap.SetColor(item.Value.LocalPlace, Color.green);
                        break;
                    case TileStatus.ENEMY:
                        tilemap.SetColor(item.Value.LocalPlace, Color.red);
                        break;
                }
            }
        }
    }
}