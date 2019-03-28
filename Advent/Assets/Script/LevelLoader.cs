using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent
{
    public class LevelLoader : MonoBehaviour
    {
        public GameObject gameManager;
        void Awake()
        {
            if (GameManager.instance == null)
            {
                Instantiate(gameManager);
            }
        }
    }
}
