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