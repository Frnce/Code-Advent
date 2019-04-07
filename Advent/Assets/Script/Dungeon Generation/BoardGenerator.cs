using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Advent.Entities;
using Advent.Utilities;
using Advent.Items;

namespace Advent.Dungeons
{
    public class BoardGenerator : MonoBehaviour
    {
        public Tile gridTile = null;
        [SerializeField]
        private BoardParameters boardParameters = null;
        [SerializeField]
        private Tilemap groundMap = null;
        [SerializeField]
        private Tilemap pitMap = null;
        [SerializeField]
        private Tilemap wallMap = null;
        [SerializeField]
        private Tilemap gridViewer = null;
        [SerializeField]
        private Item summonItem;
        private int routeCount = 0;
        List<Vector2> vacantTiles;
        GridLayout gridLayout;
        Player player;
        private GameObject enemyCollection;
        // Start is called before the first frame update
        void Start()
        {
            player = Player.instance;
            enemyCollection = new GameObject("Enemy Collection");
            SetupBoard();
            GameTiles.instance.GetWorldTiles(); 
        }
        public void SetupBoard()
        {
            gridLayout = GetComponent<GridLayout>();
            int x = 0;
            int y = 0;
            int routeLength = 0;
            GenerateSquare(x, y, 1);
            Vector2Int previousPos = new Vector2Int(x, y);
            y += 3;
            GenerateSquare(x, y, 1);
            NewRoute(x, y, routeLength, previousPos);

            FillWalls();

            GetAvailableTiles();
            MovePlayerToStartPosition();
            SpawnEnemies();
            SpawnTestItem();
        }
        public void ClearAllTiles()
        {
            groundMap.ClearAllTiles();
            pitMap.ClearAllTiles();
            wallMap.ClearAllTiles();
        }

        private void GetAvailableTiles() //YOu can really improve this in the future
        {
            vacantTiles = new List<Vector2>();
            for (int n = groundMap.cellBounds.xMin; n < groundMap.cellBounds.xMax; n++)
            {
                for (int p = groundMap.cellBounds.yMin; p < groundMap.cellBounds.yMax; p++)
                {
                    Vector3Int localPlace = (new Vector3Int(n, p, (int)groundMap.transform.position.y));
                    Vector3 place = groundMap.CellToWorld(localPlace);
                    gridViewer.SetTile(localPlace, gridTile);
                    if (groundMap.HasTile(localPlace))
                    {
                        //Tile at "place"
                        vacantTiles.Add(place);
                    }
                    else
                    {
                        //No tile at "place"
                    }
                }
            }
        }
        private void MovePlayerToStartPosition()
        {
            int selectedAvailableTile = Random.Range(0, vacantTiles.Count);
            player.transform.position = new Vector3(vacantTiles[selectedAvailableTile].x, vacantTiles[selectedAvailableTile].y, 0);
            vacantTiles.Remove(vacantTiles[selectedAvailableTile]);
        }
        private void SpawnEnemies()
        {
            for (int i = 0; i < boardParameters.enemyCount.Random; i++)
            {
                int selectedAvailableTile = Random.Range(0, vacantTiles.Count);
                int getRandomEnemy = Random.Range(0, boardParameters.enemies.Length);
                Vector3 spawnPosition = new Vector3(vacantTiles[selectedAvailableTile].x, vacantTiles[selectedAvailableTile].y, 0);
                Instantiate(boardParameters.enemies[getRandomEnemy], spawnPosition,Quaternion.identity,enemyCollection.transform);
                vacantTiles.Remove(vacantTiles[selectedAvailableTile]);
            }
        }
        private void SpawnTestItem()
        {
            int selectAvailableTile = Random.Range(0, vacantTiles.Count);
            Vector3 spawnPosition = new Vector3(vacantTiles[selectAvailableTile].x, vacantTiles[selectAvailableTile].y, 0);
            Instantiate(summonItem.gameobject, spawnPosition, Quaternion.identity);
            vacantTiles.Remove(vacantTiles[selectAvailableTile]);
        }
        private void FillWalls()
        {
            BoundsInt bounds = groundMap.cellBounds;
            for (int xMap = bounds.xMin - 10; xMap <= bounds.xMax + 10; xMap++)
            {
                for (int yMap = bounds.yMin - 10; yMap <= bounds.yMax + 10; yMap++)
                {
                    Vector3Int pos = new Vector3Int(xMap, yMap, 0);
                    Vector3Int posBelow = new Vector3Int(xMap, yMap - 1, 0);
                    Vector3Int posAbove = new Vector3Int(xMap, yMap + 1, 0);
                    TileBase tile = groundMap.GetTile(pos);
                    TileBase tileBelow = groundMap.GetTile(posBelow);
                    TileBase tileAbove = groundMap.GetTile(posAbove);
                    if (tile == null)
                    {
                        SetTileFromArray(pitMap, boardParameters.pitTile, pos);
                        if (tileBelow != null)
                        {
                            SetTileFromArray(wallMap, boardParameters.topWallTile, pos);
                        }
                        else if (tileAbove != null)
                        {
                            SetTileFromArray(wallMap, boardParameters.botWallTile, pos);
                        }
                    }
                }
            }
        }
        private void NewRoute(int x, int y, int routeLength, Vector2Int previousPos)
        {
            if (routeCount < boardParameters.maxRoutes.Random)
            {
                routeCount++;
                while (++routeLength < boardParameters.maxRouteLength.Random)
                {
                    //Initialize
                    bool routeUsed = false;
                    int xOffset = x - previousPos.x; //0
                    int yOffset = y - previousPos.y; //3
                    int roomSize = 1; //Hallway size
                    if (Random.Range(1, 100) <= boardParameters.roomRate.Random)
                        roomSize = Random.Range(3, 6);
                    previousPos = new Vector2Int(x, y);

                    //Go Straight
                    if (Random.Range(1, 100) <= boardParameters.deviationRate.Random)
                    {
                        if (routeUsed)
                        {
                            GenerateSquare(previousPos.x + xOffset, previousPos.y + yOffset, roomSize);
                            NewRoute(previousPos.x + xOffset, previousPos.y + yOffset, Random.Range(routeLength, boardParameters.maxRouteLength.Random), previousPos);
                        }
                        else
                        {
                            x = previousPos.x + xOffset;
                            y = previousPos.y + yOffset;
                            GenerateSquare(x, y, roomSize);
                            routeUsed = true;
                        }
                    }

                    //Go left
                    if (Random.Range(1, 100) <= boardParameters.deviationRate.Random)
                    {
                        if (routeUsed)
                        {
                            GenerateSquare(previousPos.x - yOffset, previousPos.y + xOffset, roomSize);
                            NewRoute(previousPos.x - yOffset, previousPos.y + xOffset, Random.Range(routeLength, boardParameters.maxRouteLength.Random), previousPos);
                        }
                        else
                        {
                            y = previousPos.y + xOffset;
                            x = previousPos.x - yOffset;
                            GenerateSquare(x, y, roomSize);
                            routeUsed = true;
                        }
                    }
                    //Go right
                    if (Random.Range(1, 100) <= boardParameters.deviationRate.Random)
                    {
                        if (routeUsed)
                        {
                            GenerateSquare(previousPos.x + yOffset, previousPos.y - xOffset, roomSize);
                            NewRoute(previousPos.x + yOffset, previousPos.y - xOffset, Random.Range(routeLength, boardParameters.maxRouteLength.Random), previousPos);
                        }
                        else
                        {
                            y = previousPos.y - xOffset;
                            x = previousPos.x + yOffset;
                            GenerateSquare(x, y, roomSize);
                            routeUsed = true;
                        }
                    }

                    if (!routeUsed)
                    {
                        x = previousPos.x + xOffset;
                        y = previousPos.y + yOffset;
                        GenerateSquare(x, y, roomSize);
                    }
                }
            }
        }
        private void GenerateSquare(int x, int y, int radius)
        {
            for (int tileX = x - radius; tileX <= x + radius; tileX++)
            {
                for (int tileY = y - radius; tileY <= y + radius; tileY++)
                {
                    Vector3Int tilePos = new Vector3Int(tileX, tileY, 0);
                    SetTileFromArray(groundMap, boardParameters.groundTile, tilePos);
                }
            }
        }
        private void SetTileFromArray(Tilemap tilemap,Tile[] tile, Vector3Int position)
        {
            int randomIndex = Random.Range(0, tile.Length);
            tilemap.SetTile(position, tile[randomIndex]);
        }
    }
}