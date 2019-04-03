using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Advent.Entities;

namespace Advent.Utilities
{
    public class TileTrigger : MonoBehaviour
    {
        private WorldTile currentTile;
        private WorldTile pastTile;
        private Vector3Int tileLocation;
        private Vector3Int pastTileLocation;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                Vector3 point = Player.instance.transform.position;
                var worldPoint = new Vector3Int(Mathf.FloorToInt(point.x), Mathf.FloorToInt(point.y), 0);

                var tiles = GameTiles.instance.tiles; // This is our Dictionary of tiles

                if (tiles.TryGetValue(worldPoint, out currentTile))
                {
                    tileLocation = currentTile.LocalPlace;
                    currentTile.tileStatus = TileStatus.OCCUPIED;
                    Debug.Log("Player Enter -" + currentTile.LocalPlace + " Status :" + currentTile.tileStatus);
                }
            }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            pastTileLocation = tileLocation;
            if (collision.CompareTag("Player"))
            {
                Vector3 point = Player.instance.transform.position;
                var worldPoint = new Vector3Int(Mathf.FloorToInt(point.x), Mathf.FloorToInt(point.y), 0);

                var tiles = GameTiles.instance.tiles; // This is our Dictionary of tiles
                if (tiles.TryGetValue(pastTileLocation, out currentTile))
                {
                    currentTile.tileStatus = TileStatus.EMPTY;
                    Debug.Log("Player Exit -" + currentTile.LocalPlace + " Status :" + currentTile.tileStatus);
                }
            }
        }
    }
}
