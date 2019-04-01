using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Advent.Dungeons;
using Advent.Entities;

namespace Advent.Camera
{
    public class CameraController : MonoBehaviour
    {
        Player player;
        // Start is called before the first frame update
        void Start()
        {
            player = Player.instance;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            transform.position = new Vector3(Player.instance.transform.position.x, Player.instance.transform.position.y,-10);
        }
    }
}
