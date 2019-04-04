using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Advent.Entities;

namespace Advent.Utilities
{
    public class GameTiles : MonoBehaviour
    {
        public static GameTiles instance;
        public Tilemap tilemap;
        public WorldTile currentTile;

        public Dictionary<Vector3, WorldTile> tiles;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else if (instance != this)
            {
                Destroy(gameObject);
            }
        }
        private void Update()
        {
            Vector3 playerPoint = Player.instance.transform.position;
            List<Vector3Int> enemyPoints = new List<Vector3Int>();
            var playerWorldPoint = new Vector3Int(Mathf.FloorToInt(playerPoint.x), Mathf.FloorToInt(playerPoint.y), 0);
            foreach (KeyValuePair<Vector3,WorldTile> item in tiles)
            {
                for (int i = 0; i < GameManager.instance.GetEnemyList().Count; i++)
                {
                    enemyPoints.Add( new Vector3Int(Mathf.FloorToInt(GameManager.instance.GetEnemyList()[i].transform.position.x),Mathf.FloorToInt(GameManager.instance.GetEnemyList()[i].transform.position.y),0));
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
                    else
                    {
                        item.Value.tileStatus = TileStatus.EMPTY;
                    }
                }
            }
            SeekTileStatus();
        }
        public void SeekTileStatus()
        {
            foreach (KeyValuePair<Vector3,WorldTile> item in tiles)
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
        public void GetWorldTiles()
        {
            tiles = new Dictionary<Vector3, WorldTile>();
            foreach (Vector3Int pos in tilemap.cellBounds.allPositionsWithin)
            {
                var localPlace = new Vector3Int(pos.x, pos.y, pos.z);

                if (!tilemap.HasTile(localPlace))
                {
                    continue;
                }
                var tile = new WorldTile
                {
                    LocalPlace = localPlace,
                    WorldLocation = tilemap.CellToWorld(localPlace),
                    TileBase = tilemap.GetTile(localPlace),
                    TilemapMember = tilemap,
                    Name = localPlace.x + "," + localPlace.y,
                    Cost = 1,
                    tileStatus = TileStatus.EMPTY
                };
                tiles.Add(tile.WorldLocation, tile);
            }
        }
        public void SetTileStatus(WorldTile currentTile,TileStatus status)
        {
            currentTile.tileStatus = status;
        }
    }
}