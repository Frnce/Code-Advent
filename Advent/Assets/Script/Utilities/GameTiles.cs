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
        public Tilemap Tilemap;

        public Dictionary<Vector3, WorldTile> tiles;
<<<<<<< HEAD
        public delegate void OnTileStatusChange(TileStatus status);
        public OnTileStatusChange onTileChange;
=======
>>>>>>> parent of 7ca7620... Can Now follow the If a Tile is Occupied by the player

        private WorldTile currentTile;
        private WorldTile pastTile;
        private Vector3Int tileLocation;
        private Vector3Int pastTileLocation;

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
<<<<<<< HEAD
        private void Start()
        {
            onTileChange += GetEntityLocation;
        }
        public void GetEntityLocation(TileStatus status)
        {
            StartCoroutine(EnterNewTile(status));
            pastTileLocation = tileLocation;
            StartCoroutine(ExitTile(TileStatus.EMPTY));
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
=======

>>>>>>> parent of 7ca7620... Can Now follow the If a Tile is Occupied by the player
        public void GetWorldTiles()
        {
            tiles = new Dictionary<Vector3, WorldTile>();
            foreach (Vector3Int pos in Tilemap.cellBounds.allPositionsWithin)
            {
                var localPlace = new Vector3Int(pos.x, pos.y, pos.z);

                if (!Tilemap.HasTile(localPlace))
                {
                    continue;
                }
                var tile = new WorldTile
                {
                    LocalPlace = localPlace,
                    WorldLocation = Tilemap.CellToWorld(localPlace),
                    TileBase = Tilemap.GetTile(localPlace),
                    TilemapMember = Tilemap,
                    Name = localPlace.x + "," + localPlace.y,
                    Cost = 1,
                    tileStatus = TileStatus.EMPTY
                };
                tiles.Add(tile.WorldLocation, tile);
            }
        }
<<<<<<< HEAD
        public void SetTileStatus(WorldTile currentTile,TileStatus status)
        {
            currentTile.tileStatus = status;
        }
        IEnumerator EnterNewTile(TileStatus status)
        {
            yield return new WaitForSeconds(GameManager.instance.turnDelay);
            Vector3 point = Player.instance.transform.position;
            var worldPoint = new Vector3Int(Mathf.FloorToInt(point.x), Mathf.FloorToInt(point.y), 0);

            var newTile = tiles; // This is our Dictionary of tiles
            if (newTile.TryGetValue(worldPoint, out currentTile))
            {
                tileLocation = currentTile.LocalPlace;
                currentTile.tileStatus = status;
                SeekTileStatus();
            }
        }
        IEnumerator ExitTile(TileStatus status)
        {
            yield return new WaitForSeconds(GameManager.instance.turnDelay);
            Vector3 point = pastTileLocation;
            var worldPoint = new Vector3Int(Mathf.FloorToInt(point.x), Mathf.FloorToInt(point.y), 0);

            var tiles = GameTiles.instance.tiles; // This is our Dictionary of tiles
            if (tiles.TryGetValue(worldPoint, out currentTile))
            {
                currentTile.tileStatus = status;
                SeekTileStatus();
            }
        }
=======
>>>>>>> parent of 7ca7620... Can Now follow the If a Tile is Occupied by the player
    }
}