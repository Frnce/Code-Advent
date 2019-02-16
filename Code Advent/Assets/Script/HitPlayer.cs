using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Advent.Player;

public class HitPlayer : MonoBehaviour
{
    //This certain module will temporarily disable the boxcollider then will enable after certain time to hurt the player if they are in range
    //This is only a temporary code. this will be used and update later on the development
    [HideInInspector]
    public PlayerController player;
    public EnemyObject enemy;

    bool isHit = false;

    float maxEnabledTime = 2f;
    float enabledTime;
    private void Awake()
    {
        player = FindObjectOfType<PlayerController>();
    }
    private void Start()
    {
        GetComponent<BoxCollider2D>().enabled = false;
        enabledTime = maxEnabledTime;
    }
    private void Update()
    {
        enabledTime -= Time.deltaTime;
        if (enabledTime <= 0)
        {
            GetComponent<BoxCollider2D>().enabled = true;
            enabledTime = maxEnabledTime;
        }
        else
        {
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player") && !isHit)
        {
            isHit = true;
            player.playerStats.TakeDamage(enemy.attack);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        isHit = false;
    }
}
