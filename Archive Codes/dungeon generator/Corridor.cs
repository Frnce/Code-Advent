using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent.Dungeons
{
    public enum Direction
    {
        NORTH,
        EAST,
        SOUTH,
        WEST,
    }

    public class Corridor
    {
        public int startXPosition;
        public int startYPosition;
        public int corridorLength;
        public Direction direction;

        public int EndPositionX
        {
            get
            {
                if (direction == Direction.NORTH || direction == Direction.SOUTH)
                {
                    return startXPosition;
                }
                if(direction == Direction.EAST)
                {
                    return startXPosition + corridorLength - 1;
                }
                return startXPosition - corridorLength + 1;
            }
        }
        public int EndPositionY
        {
            get
            {
                if(direction == Direction.EAST || direction == Direction.WEST)
                {
                    return startYPosition;
                }
                if(direction == Direction.NORTH)
                {
                    return startYPosition + corridorLength - 1;
                }
                return startYPosition - corridorLength + 1;
            }
        }
        public void SetupCorridor(Room room, IntRange length, IntRange roomWidth, IntRange roomHeight, int columns, int rows, bool firstCorridor)
        {
            direction = (Direction)Random.Range(0, 4);

            Direction oppositeDirection = (Direction)(((int)room.enteringCorridor + 2) % 4);

            if (!firstCorridor && direction == oppositeDirection)
            {
                int directionInt = (int)direction;
                directionInt++;
                directionInt = directionInt % 4;
                direction = (Direction)directionInt;
            }
            corridorLength = length.Random;

            int maxLength = length.m_Max;

            switch (direction)
            {
                case Direction.NORTH:
                    startXPosition = Random.Range(room.xPosition, room.xPosition + room.roomWidth - 1);
                    startYPosition = room.yPosition + room.roomHeight;

                    maxLength = rows - startYPosition - room.roomHeight;
                    break;
                case Direction.EAST:
                    startXPosition = room.xPosition + room.roomWidth;
                    startYPosition = Random.Range(room.yPosition, room.yPosition + room.roomHeight - 1);
                    maxLength = columns - startXPosition - roomWidth.m_Min;
                    break;
                case Direction.SOUTH:
                    startXPosition = Random.Range(room.xPosition, room.xPosition + room.roomWidth);
                    startYPosition = room.yPosition;
                    maxLength = startYPosition - roomHeight.m_Min;
                    break;
                case Direction.WEST:
                    startXPosition = room.xPosition;
                    startYPosition = Random.Range(room.yPosition, room.yPosition + room.roomHeight);
                    maxLength = startXPosition - roomWidth.m_Min;
                    break;
                default:
                    break;
            }
            corridorLength = Mathf.Clamp(corridorLength, 1, maxLength);
        }
    }
}