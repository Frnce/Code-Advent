using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Advent;
using Advent.Entities;

namespace Advent.Dungeons
{
    public class BoardCreator : MonoBehaviour
    {
        public enum TileType
        {
            WALL,
            FLOOR,
        }
        public GameObject nextLevelObject;
        public GameObject[] chest;

        [SerializeField]
        private BoardParameters boardParameters = null;
        private TileType[][] tiles;
        private Room[] rooms;
        private Corridor[] corridors;
        private GameObject boardHolder;
        private GameObject enemyHolder;

        // Start is called before the first frame update
        void Start()
        {
            boardHolder = new GameObject("BoardHolder");
            enemyHolder = new GameObject("EnemyHolder");

            SetupTilesArray();
            CreateRoomsAndCorridors();

            SetTilesValuesForRooms();
            SetTilesValluesForCorridors();

            InstantiateTiles();
            InstantiateOuterWalls();

            MovePlayerToStartPosition();

            InstantiateNextLevelObject();

            InstantiateEnemies();
            InstantiateChest();
        }

        private void MovePlayerToStartPosition()
        {
            int randomRoom = Random.Range(0, rooms.Length);
            int randomWidth = Random.Range(0, rooms[randomRoom].roomWidth) + rooms[randomRoom].xPosition;
            int randomHeight = Random.Range(0, rooms[randomRoom].roomHeight) + rooms[randomRoom].yPosition;
            Vector3 playerPos = new Vector3(randomWidth, randomHeight, 0);
            Player.instance.transform.position = playerPos;
        }

        private void SetupTilesArray()
        {
            tiles = new TileType[boardParameters.columns][];

            for (int i = 0; i < tiles.Length; i++)
            {
                tiles[i] = new TileType[boardParameters.rows];
            }
        }

        private void CreateRoomsAndCorridors()
        {
            rooms = new Room[boardParameters.numOfRooms.Random];
            corridors = new Corridor[rooms.Length - 1];

            rooms[0] = new Room();
            corridors[0] = new Corridor();

            rooms[0].SetupRoom(boardParameters.roomWidth, boardParameters.roomHeight, boardParameters.columns, boardParameters.rows);
            corridors[0].SetupCorridor(rooms[0], boardParameters.corridorLength, boardParameters.roomWidth, boardParameters.roomHeight, boardParameters.columns, boardParameters.rows, true);

            for (int i = 1; i < rooms.Length; i++)
            {
                rooms[i] = new Room();
                rooms[i].SetupRoom(boardParameters.roomWidth, boardParameters.roomHeight, boardParameters.columns, boardParameters.rows, corridors[i - 1]);

                if(i < corridors.Length)
                {
                    corridors[i] = new Corridor();
                    corridors[i].SetupCorridor(rooms[i], boardParameters.corridorLength, boardParameters.roomWidth, boardParameters.roomHeight, boardParameters.columns, boardParameters.rows, false);
                }
            }
        }
        private void SetTilesValuesForRooms()
        {
            for (int i = 0; i < rooms.Length; i++)
            {
                Room currentRoom = rooms[i];

                for (int j = 0; j < currentRoom.roomWidth; j++)
                {
                    int xCoordinate = currentRoom.xPosition + j;

                    for (int k = 0; k < currentRoom.roomHeight; k++)
                    {
                        int yCoordinate = currentRoom.yPosition + k;

                        tiles[xCoordinate][yCoordinate] = TileType.FLOOR;
                    }
                }
            }
        }
        private void SetTilesValluesForCorridors()
        {
            for (int i = 0; i < corridors.Length; i++)
            {
                Corridor currentCorridor = corridors[i];

                for (int j = 0; j < currentCorridor.corridorLength; j++)
                {
                    int xCoordinate = currentCorridor.startXPosition;
                    int yCoordinate = currentCorridor.startYPosition;

                    switch (currentCorridor.direction)
                    {
                        case Direction.NORTH:
                            yCoordinate += j;
                            break;
                        case Direction.EAST:
                            xCoordinate += j;
                            break;
                        case Direction.SOUTH:
                            yCoordinate -= j;
                            break;
                        case Direction.WEST:
                            xCoordinate -= j;
                            break;
                    }

                    tiles[xCoordinate][yCoordinate] = TileType.FLOOR;
                }
            }
        }
        private void InstantiateTiles()
        {
            for (int i = 0; i < tiles.Length; i++)
            {
                for (int j = 0; j < tiles[i].Length; j++)
                {
                    InstantiateFromArray(boardParameters.floorTiles, i, j);
                    if (tiles[i][j] == TileType.WALL)
                    {
                        InstantiateFromArray(boardParameters.wallTiles, i, j);
                    }
                }
            }
        }
        private void InstantiateOuterWalls()
        {
            float leftEdgeX = -1f;
            float rightEdgeX = boardParameters.columns + 0f;
            float bottomEdgeY = -1f;
            float topEdgeY = boardParameters.rows + 0f;

            InstantiateVerticalOuterWall(leftEdgeX, bottomEdgeY, topEdgeY);
            InstantiateVerticalOuterWall(rightEdgeX, bottomEdgeY, topEdgeY);

            InstantiateHorizontalOuterWall(leftEdgeX + 1f, rightEdgeX - 1f, bottomEdgeY);
            InstantiateHorizontalOuterWall(leftEdgeX + 1f, rightEdgeX - 1f, topEdgeY);
        }
        private void InstantiateEnemies()
        {
            List<int> enemyWidthPosition = new List<int>();
            List<int> enemyHeightPosition = new List<int>();
            for (int i = 0; i < boardParameters.enemyCount.m_Max; i++)
            {
                int randomRoom = Random.Range(0, rooms.Length);
                int randomWidth = Random.Range(0, rooms[randomRoom].roomWidth) + rooms[randomRoom].xPosition;
                int randomHeight = Random.Range(0, rooms[randomRoom].roomHeight) + rooms[randomRoom].yPosition;
                enemyWidthPosition.Add(randomWidth);
                enemyHeightPosition.Add(randomHeight);
            }
            for (int i = 0; i < enemyWidthPosition.Count; i++)
            {
                InstantiateFromArray(boardParameters.enemies, enemyWidthPosition[i], enemyHeightPosition[i]);
            }
        }
        private void InstantiateChest()
        {
            List<int> chestWidthPosition = new List<int>();
            List<int> chestHeightPosition = new List<int>();

            for (int i = 0; i < boardParameters.chestCount.m_Max; i++)
            {
                int randomRoom = Random.Range(0, rooms.Length);
                int randomWidth = Random.Range(0, rooms[randomRoom].roomWidth) + rooms[randomRoom].xPosition;
                int randomHeight = Random.Range(0, rooms[randomRoom].roomHeight) + rooms[randomRoom].yPosition;
                chestWidthPosition.Add(randomWidth);
                chestHeightPosition.Add(randomHeight);
            }
            for (int i = 0; i < chestWidthPosition.Count; i++)
            {
                InstantiateFromArray(chest, chestWidthPosition[i], chestHeightPosition[i]);
            }
        }
        private void InstantiateNextLevelObject()
        {
            int randomRoom = Random.Range(0, rooms.Length);
            int randomWidth = Random.Range(0, rooms[randomRoom].roomWidth) + rooms[randomRoom].xPosition;
            int randomHeight = Random.Range(0, rooms[randomRoom].roomHeight) + rooms[randomRoom].yPosition;
            Vector3 ladderPos = new Vector3(randomWidth, randomHeight, 0);
            Instantiate(nextLevelObject, ladderPos, Quaternion.identity);
        }
        private void InstantiateVerticalOuterWall(float xCoordinate, float startingY,float endingY)
        {
            float currentY = startingY;
            while(currentY <= endingY)
            {
                InstantiateFromArray(boardParameters.outerWallTiles, xCoordinate, currentY);
                currentY++;
            }
        }
        private void InstantiateHorizontalOuterWall(float startingX, float endingX, float yCoord)
        {
            float currentX = startingX;
            while(currentX <= endingX)
            {
                InstantiateFromArray(boardParameters.outerWallTiles, currentX, yCoord);
                currentX++;
            }
        }
        void InstantiateFromArray(GameObject[] prefabs, float xCoord, float yCoord)
        {
            // Create a random index for the array.
            int randomIndex = Random.Range(0, prefabs.Length);

            // The position to be instantiated at is based on the coordinates.
            Vector3 position = new Vector3(xCoord, yCoord, 0f);

            // Create an instance of the prefab from the random index of the array.
            GameObject tileInstance = Instantiate(prefabs[randomIndex], position, Quaternion.identity) as GameObject;

            // Set the tile's parent to the board holder.
            if(prefabs == boardParameters.enemies)
            {
                tileInstance.transform.parent = enemyHolder.transform;
            }
            else
            {
                tileInstance.transform.parent = boardHolder.transform;
            }
        }
        public Vector2 GetMaxBoardPosition()
        {
            return new Vector2(boardParameters.rows, boardParameters.columns);
        }
    }
}