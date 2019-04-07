using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Advent.Entities;

namespace Advent
{
    public class LevelLoader : MonoBehaviour
    {
        public GameObject gameManager;
        public GameObject player;
        public GameObject gui;
        void Awake()
        {
            if (Player.instance == null)
            {
                Instantiate(player);
            }
            if (GameManager.instance == null)
            {
                Instantiate(gameManager);
            }
            Instantiate(gui);
        }
    }
}