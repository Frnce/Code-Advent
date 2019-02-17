using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Advent.Player;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private PlayerController player;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    //public void RestartScene()
    //{
    //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // first scene
    //}
}
