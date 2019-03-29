﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Advent.Entities;
using Advent.Dungeons;
using UnityEngine.SceneManagement;
namespace Advent
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance = null;
        public float turnDelay = 0.1f;
        [HideInInspector] public bool playersTurn = true;

        private List<Enemy> enemies;
        private bool enemiesMoving;
        [SerializeField]
        private BoardParameters boardParameters = null;
        public GameObject nextLevelObject;
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }

            DontDestroyOnLoad(gameObject);

            enemies = new List<Enemy>();
        }
        private void OnLevelWasLoaded(int level)
        {
            InitGame();
        }
        private void Start()
        {
            InitGame();
        }
        void InitGame()
        {
            enemies.Clear();
            BoardCreator board = GetComponent<BoardCreator>();
            board.SetupBoard(boardParameters,nextLevelObject);
        }

        // Update is called once per frame
        void Update()
        {
            if (playersTurn || enemiesMoving)
            {
                return;
            }
            StartCoroutine(MoveEnemies());
        }
        public void ReloadScene()
        {
            SceneManager.LoadScene(0);
        }
        public void AddEnemyToList(Enemy script)
        {
            //Add Enemy to List enemies.
            enemies.Add(script);
        }
        public void RemoveEnemyToList(Enemy enemy)
        {
            enemies.Remove(enemy);
        }
        public void GameOver()
        {
            enabled = false;
        }

        //Coroutine to move enemies in sequence.
        IEnumerator MoveEnemies()
        {
            //While enemiesMoving is true player is unable to move.
            enemiesMoving = true;

            //Wait for turnDelay seconds, defaults to .1 (100 ms).
            yield return new WaitForSeconds(turnDelay);

            //If there are no enemies spawned (IE in first level):
            if (enemies.Count == 0)
            {
                //Wait for turnDelay seconds between moves, replaces delay caused by enemies moving when there are none.
                yield return new WaitForSeconds(turnDelay);
            }

            //Loop through List of Enemy objects.
            for (int i = 0; i < enemies.Count; i++)
            {
                //Call the MoveEnemy function of Enemy at index i in the enemies List.
                if(enemies[i] == null)
                {
                    //yield return new WaitForSeconds(enemies[i].moveTime);
                    break;
                }
                enemies[i].MoveEnemy();
                //Wait for Enemy's moveTime before moving next Enemy, 
                //
            }
            yield return new WaitForSeconds(0.1f);
            //Once Enemies are done moving, set playersTurn to true so player can move.
            playersTurn = true;
            yield return new WaitForSeconds(0.1f);
            //Enemies are done moving, set enemiesMoving to false.
            enemiesMoving = false;
        }
    }
}