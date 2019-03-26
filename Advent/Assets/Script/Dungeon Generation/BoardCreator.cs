using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Advent;

namespace Advent.Dungeons
{
    public class BoardCreator : MonoBehaviour
    {
        public enum TileType
        {
            WALL,
            FLOOR,
        }
        public int columns = 100;
        public int rows = 100;
        public IntRange numOfRooms = new IntRange(15, 20);
        public IntRange roomWidth = new IntRange(3, 10);
        public IntRange roomHeight = new IntRange(3, 10);
        public IntRange corridorLength = new IntRange(6, 10);
        public GameObject[] floorTiles;
        public GameObject[] wallTiles;
        public GameObject[] outerWallTiles;
        public GameObject player;
        public GameObject GUI;

        private TileType[][] tiles;
        private Room[] rooms;
        private Corridor[] corridors;
        private GameObject boardHolder;

        // Start is called before the first frame update
        void Start()
        {
            boardHolder = new GameObject("BoardHolder");

            SetupTilesArray();
            CreateRoomsAndCorridors();

            SetTilesValuesForRooms();
            SetTilesValluesForCorridors();

            InstantiateTiles();
            InstantiateOuterWalls();

            InstantiateUI();
        }

        private void SetupTilesArray()
        {
            tiles = new TileType[columns][];

            for (int i = 0; i < tiles.Length; i++)
            {
                tiles[i] = new TileType[rows];
            }
        }

        private void CreateRoomsAndCorridors()
        {
            rooms = new Room[numOfRooms.Random];
            corridors = new Corridor[rooms.Length - 1];

            rooms[0] = new Room();
            corridors[0] = new Corridor();

            rooms[0].SetupRoom(roomWidth, roomHeight, columns, rows);
            corridors[0].SetupCorridor(rooms[0], corridorLength, roomWidth, roomHeight, columns, rows, true);

            for (int i = 1; i < rooms.Length; i++)
            {
                rooms[i] = new Room();
                rooms[i].SetupRoom(roomWidth, roomHeight, columns, rows, corridors[i - 1]);

                if(i < corridors.Length)
                {
                    corridors[i] = new Corridor();
                    corridors[i].SetupCorridor(rooms[i], corridorLength, roomWidth, roomHeight, columns, rows, false);
                }

                if (i == rooms.Length * 0.5f)
                {
                    Vector3 playerPos = new Vector3(rooms[i].xPosition, rooms[i].yPosition, 0);
                    Instantiate(player, playerPos, Quaternion.identity);
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
                    InstantiateFromArray(floorTiles, i, j);
                    if (tiles[i][j] == TileType.WALL)
                    {
                        InstantiateFromArray(wallTiles, i, j);
                    }
                }
            }
        }
        private void InstantiateOuterWalls()
        {
            float leftEdgeX = -1f;
            float rightEdgeX = columns + 0f;
            float bottomEdgeY = -1f;
            float topEdgeY = rows + 0f;

            InstantiateVerticalOuterWall(leftEdgeX, bottomEdgeY, topEdgeY);
            InstantiateVerticalOuterWall(rightEdgeX, bottomEdgeY, topEdgeY);

            InstantiateHorizontalOuterWall(leftEdgeX + 1f, rightEdgeX - 1f, bottomEdgeY);
            InstantiateHorizontalOuterWall(leftEdgeX + 1f, rightEdgeX - 1f, topEdgeY);
        }
        private void InstantiateUI()
        {
            Instantiate(GUI);
        }
        private void InstantiateVerticalOuterWall(float xCoordinate, float startingY,float endingY)
        {
            float currentY = startingY;
            while(currentY <= endingY)
            {
                InstantiateFromArray(outerWallTiles, xCoordinate, currentY);
                currentY++;
            }
        }
        private void InstantiateHorizontalOuterWall(float startingX, float endingX, float yCoord)
        {
            float currentX = startingX;
            while(currentX <= endingX)
            {
                InstantiateFromArray(outerWallTiles, currentX, yCoord);
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
            tileInstance.transform.parent = boardHolder.transform;
        }
    }
}