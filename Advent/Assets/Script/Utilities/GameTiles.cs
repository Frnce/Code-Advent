using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Advent.Utilities
{
    public class GameTiles : MonoBehaviour
    {
        public static GameTiles instance;
        public Tilemap tilemap;

        public Dictionary<Vector3, WorldTile> tiles;
        public delegate void OnTileStatusChange();
        public OnTileStatusChange onTileChange;

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
        private void Start()
        {
            onTileChange += SeekTileStatus;
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