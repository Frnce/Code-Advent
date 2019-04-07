using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent.Dungeons
{
    public class Room
    {
        public int xPosition;
        public int yPosition;
        public int roomWidth;
        public int roomHeight;
        public Direction enteringCorridor;

        public void SetupRoom(IntRange widthRange,IntRange heightRange, int columns, int rows)
        {
            roomWidth = widthRange.Random;
            roomHeight = heightRange.Random;

            xPosition = Mathf.RoundToInt(columns / 2f - roomWidth / 2f);
            yPosition = Mathf.RoundToInt(rows / 2f - roomHeight / 2f);
        }
        public void SetupRoom(IntRange widthRange, IntRange heightRange, int columns, int rows, Corridor corridor)
        {
            enteringCorridor = corridor.direction;

            roomWidth = widthRange.Random;
            roomHeight = heightRange.Random;

            switch (corridor.direction)
            {
                case Direction.NORTH:
                    roomHeight = Mathf.Clamp(roomHeight, 1, rows - corridor.EndPositionY);
                    yPosition = corridor.EndPositionY;
                    xPosition = Random.Range(corridor.EndPositionX - roomWidth + 1, corridor.EndPositionX);
                    xPosition = Mathf.Clamp(xPosition, 0, columns - roomWidth);
                    break;
                case Direction.EAST:
                    roomWidth = Mathf.Clamp(roomWidth, 1, columns - corridor.EndPositionX);
                    xPosition = corridor.EndPositionX;

                    yPosition = Random.Range(corridor.EndPositionY - roomHeight + 1, corridor.EndPositionY);
                    yPosition = Mathf.Clamp(yPosition, 0, rows - roomHeight);
                    break;
                case Direction.SOUTH:
                    roomHeight = Mathf.Clamp(roomHeight, 1, corridor.EndPositionY);
                    yPosition = corridor.EndPositionY - roomHeight + 1;

                    xPosition = Random.Range(corridor.EndPositionX - roomWidth + 1, corridor.EndPositionX);
                    xPosition = Mathf.Clamp(xPosition, 0, columns - roomWidth);
                    break;
                case Direction.WEST:
                    roomWidth = Mathf.Clamp(roomWidth, 1, corridor.EndPositionX);
                    xPosition = corridor.EndPositionX - roomWidth + 1;

                    yPosition = Random.Range(corridor.EndPositionY - roomHeight + 1, corridor.EndPositionY);
                    yPosition = Mathf.Clamp(yPosition, 0, rows - roomHeight);
                    break;
            }
        }
    }
}