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
            BOUNDARY,
            FLOOR,
            VERTICALWALL,
            HORIZONTALWALL,
        }
        private GameObject nextLevelObject;
        private BoardParameters boardParameters = null;
        private TileType[][] tiles;
        private Room[] rooms;
        private Corridor[] corridors;
        private GameObject boardHolder;
        private GameObject enemyHolder;

        // Start is called before the first frame update
        private void Awake()
        {
        }
        public void SetupBoard(BoardParameters thisBoardParameters,GameObject door)
        {
            boardHolder = new GameObject("Board Holder");
            enemyHolder = new GameObject("Enemy Holder");

            boardParameters = thisBoardParameters;
            nextLevelObject = door;
            SetupTilesArray();
            CreateRoomsAndCorridors();

            SetTilesValuesForRooms();
            SetTilesValluesForCorridors();

            SetTilesValuesForRoomWallLeft();
            SetTilesValuesForRoomWallRight();

            SetTilesValuesForRoomWallTop();
            SetTilesValuesForRoomWallBottom();

            SetTilesValluesForCorridorWalls();

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

            //debug
            for (int i = 0; i < rooms.Length; i++)
            {
                Debug.Log("Room Number :" + i + " Width : "+ rooms[i].roomWidth+ " Height :" + rooms[i].roomHeight);
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
        private void SetTilesValuesForRoomWallLeft()
        {
            for (int i = 0; i < rooms.Length; i++)
            {
                Room currentRoom = rooms[i];
                int xCoordinate = currentRoom.xPosition - 1;
                for (int k = 0; k < currentRoom.roomHeight; k++)
                {
                    int yCoordinate = currentRoom.yPosition + k;
                    if(tiles[xCoordinate][yCoordinate] == TileType.BOUNDARY)
                    {
                        tiles[xCoordinate][yCoordinate] = TileType.VERTICALWALL;
                    }
                }
            }
        }
        private void SetTilesValuesForRoomWallRight()
        {
            for (int i = 0; i < rooms.Length; i++)
            {
                Room currentRoom = rooms[i];
                int xCoordinate = currentRoom.xPosition + currentRoom.roomWidth;
                for (int k = 0; k < currentRoom.roomHeight; k++)
                {
                    int yCoordinate = currentRoom.yPosition + k;
                    if (tiles[xCoordinate][yCoordinate] == TileType.BOUNDARY)
                    {
                        tiles[xCoordinate][yCoordinate] = TileType.VERTICALWALL;
                    }
                }
            }
        }
        private void SetTilesValuesForRoomWallTop()
        {
            for (int i = 0; i < rooms.Length; i++)
            {
                Room currentRoom = rooms[i];
                int yCoordinate = currentRoom.yPosition + currentRoom.roomHeight;
                for (int k = 0; k < currentRoom.roomWidth; k++)
                {
                    int xCoordinate = currentRoom.xPosition + k;
                    if (tiles[xCoordinate][yCoordinate] == TileType.BOUNDARY)
                    {
                        tiles[xCoordinate][yCoordinate] = TileType.HORIZONTALWALL;
                    }
                }
            }
        }
        private void SetTilesValuesForRoomWallBottom()
        {
            for (int i = 0; i < rooms.Length; i++)
            {
                Room currentRoom = rooms[i];
                int yCoordinate = currentRoom.yPosition - 1;
                for (int k = 0; k < currentRoom.roomWidth; k++)
                {
                    int xCoordinate = currentRoom.xPosition + k;
                    if (tiles[xCoordinate][yCoordinate] == TileType.BOUNDARY)
                    {
                        tiles[xCoordinate][yCoordinate] = TileType.HORIZONTALWALL;
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
        private void SetTilesValluesForCorridorWalls()
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
                    if (tiles[xCoordinate + 1][yCoordinate + 1] == TileType.BOUNDARY &&(currentCorridor.direction == Direction.NORTH || currentCorridor.direction == Direction.SOUTH))
                    {
                        tiles[xCoordinate + 1][yCoordinate + 1] = TileType.VERTICALWALL;
                    }
                    if (tiles[xCoordinate - 1][yCoordinate - 1] == TileType.BOUNDARY && (currentCorridor.direction == Direction.NORTH || currentCorridor.direction == Direction.SOUTH))
                    {
                        tiles[xCoordinate - 1][yCoordinate - 1] = TileType.VERTICALWALL;
                    }

                    if (tiles[xCoordinate + 1][yCoordinate + 1] == TileType.BOUNDARY && (currentCorridor.direction == Direction.EAST || currentCorridor.direction == Direction.WEST))
                    {
                        tiles[xCoordinate + 1][yCoordinate + 1] = TileType.HORIZONTALWALL;
                    }
                    if (tiles[xCoordinate - 1][yCoordinate - 1] == TileType.BOUNDARY && (currentCorridor.direction == Direction.EAST || currentCorridor.direction == Direction.WEST))
                    {
                        tiles[xCoordinate - 1][yCoordinate - 1] = TileType.HORIZONTALWALL;
                    }
                }
            }
        }
        private void InstantiateTiles()
        {
            for (int i = 0; i < tiles.Length; i++)
            {
                for (int j = 0; j < tiles[i].Length; j++)
                {
                    if (tiles[i][j] == TileType.BOUNDARY)
                    {
                        InstantiateFromArray(boardParameters.wallTiles, i, j);
                    }
                    else if(tiles[i][j] == TileType.VERTICALWALL)
                    {
                        InstantiateFromArray(boardParameters.outerVerticalWallTiles, i, j);
                    }
                    else if(tiles[i][j] == TileType.HORIZONTALWALL)
                    {
                        InstantiateFromArray(boardParameters.outerHorizontalWallTiles, i, j);
                    }
                    else if(tiles[i][j] == TileType.FLOOR)
                    {
                        InstantiateFromArray(boardParameters.floorTiles, i, j);
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
                InstantiateFromArray(boardParameters.chests, chestWidthPosition[i], chestHeightPosition[i]);
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
                InstantiateFromArray(boardParameters.outerVerticalWallTiles, xCoordinate, currentY);
                currentY++;
            }
        }
        private void InstantiateHorizontalOuterWall(float startingX, float endingX, float yCoord)
        {
            float currentX = startingX;
            while(currentX <= endingX)
            {
                InstantiateFromArray(boardParameters.outerHorizontalWallTiles, currentX, yCoord);
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