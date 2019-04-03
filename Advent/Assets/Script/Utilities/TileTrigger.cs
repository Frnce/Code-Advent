using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Advent.Entities;
using System;

namespace Advent.Utilities
{
    public class TileTrigger : MonoBehaviour
    {
        private WorldTile currentTile;
        private WorldTile pastTile;
        private Vector3Int tileLocation;
        private Vector3Int pastTileLocation;
        private GameTiles gameTiles;
        private void Start()
        {
            gameTiles = GameTiles.instance;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                Debug.Log("Entered");
                StartCoroutine(EnterNewTile(TileStatus.PLAYER));
            }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                pastTileLocation = tileLocation;
                Debug.Log("Exited");
                StartCoroutine(ExitTile(TileStatus.EMPTY));
            }
        }
        IEnumerator EnterNewTile(TileStatus status)
        {
            yield return new WaitForSeconds(GameManager.instance.turnDelay);
            Vector3 point = Player.instance.transform.position;
            var worldPoint = new Vector3Int(Mathf.FloorToInt(point.x), Mathf.FloorToInt(point.y), 0);

            var tiles = gameTiles.tiles; // This is our Dictionary of tiles
            if (tiles.TryGetValue(worldPoint, out currentTile))
            {
                tileLocation = currentTile.LocalPlace;
                gameTiles.SetTileStatus(currentTile, status);
                gameTiles.onTileChange.Invoke();
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
                gameTiles.SetTileStatus(currentTile, status);
                gameTiles.onTileChange.Invoke();
            }
        }
    }
}
