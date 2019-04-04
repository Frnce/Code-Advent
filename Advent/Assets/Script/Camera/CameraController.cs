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
        public Transform selector;
        // Start is called before the first frame update
        void Start()
        {
            player = Player.instance;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (!player.GetIsSelectionMode())
            {
                transform.position = new Vector3(Player.instance.transform.position.x, Player.instance.transform.position.y, -10);
            }
            else
            {
                transform.position = new Vector3(selector.position.x, selector.position.y, -10);
            }
        }
    }
}
