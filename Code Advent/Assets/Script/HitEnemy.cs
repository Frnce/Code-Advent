using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Advent.Player;

public class HitEnemy : MonoBehaviour
{
    PlayerController player;
    PlayerStats playerStats;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        playerStats = player.playerStats;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Dummy>().EnemyHit(playerStats.attack.GetValue());
        }
    }
}
