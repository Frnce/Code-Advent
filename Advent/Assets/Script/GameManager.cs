using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance = null;

        [HideInInspector] public bool playersTurn = true;
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else if( instance != this)
            {
                Destroy(gameObject);
            }

            DontDestroyOnLoad(gameObject);
        }
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void GameOver()
        {
            enabled = false;
        }
    }
}