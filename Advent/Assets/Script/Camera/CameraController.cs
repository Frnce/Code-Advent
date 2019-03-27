using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Advent.Dungeons;
using Advent.Entities;

namespace Advent.Camera
{
    public class CameraController : MonoBehaviour
    {
        private Vector2 maxPos;
        private BoardCreator board;
        Player player;
        // Start is called before the first frame update
        void Start()
        {
            player = Player.instance;
            board = FindObjectOfType<BoardCreator>();
            maxPos = board.GetMaxBoardPosition();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            transform.position = new Vector3(Player.instance.transform.position.x, Player.instance.transform.position.y,-10);

            float xPos = Mathf.Clamp(transform.position.x, 0, maxPos.x);
            float yPos = Mathf.Clamp(transform.position.y, 0, maxPos.y);
            transform.position = new Vector3(xPos, yPos,-10);
        }
    }
}
