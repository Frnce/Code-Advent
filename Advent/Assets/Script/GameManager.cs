using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Advent.Entities;
using Advent.Dungeons;
using UnityEngine.SceneManagement;
using Advent.Utilities;

namespace Advent
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance = null;
        public float turnDelay = 0.1f;
        [HideInInspector] public bool playersTurn = true;

        private List<Enemy> enemies;
        private bool enemiesMoving;
        private int level = 0;
        private int maxLevel = 0;
        public GameObject nextLevelObject;
        public int turns = 0;
        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene,LoadSceneMode mode)
        {
            if (level > maxLevel)
            {
                Debug.Log("Finished Level");
                //go to game over scene;
            }
            else
            {
                InitGame();
            }
        }
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
        void InitGame()
        {
            enemies.Clear();
            //level++;
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
        public void GoToNextLevel()
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
        public List<Enemy> GetEnemyList()
        {
            return enemies;
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
            if (enemies.Count != 0)
            {
                //Wait for turnDelay seconds between moves, replaces delay caused by enemies moving when there are none.
                for (int i = 0; i < enemies.Count; i++)
                {
                    //Call the MoveEnemy function of Enemy at index i in the enemies List.
                    if (enemies[i] == null)
                    {
                        //yield return new WaitForSeconds(enemies[i].moveTime);
                        break;
                    }
                    enemies[i].MoveEnemy();
                    //Wait for Enemy's moveTime before moving next Enemy
                }
            }   
            yield return new WaitForSeconds(0.1f);
            //Enemies are done moving, set enemiesMoving to false.
            enemiesMoving = false;
            //Once Enemies are done moving, set playersTurn to true so player can move.
            playersTurn = true;
        }
    }
}