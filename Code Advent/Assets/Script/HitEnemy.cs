using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Advent.Player;
using Advent.CharacterStat;
using Advent.Enemies;
using Advent.EnemyStat;

public class HitEnemy : MonoBehaviour
{
    PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        player = PlayerController.instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Slime>().TakeDamage(player.playerStats.attack.GetValue());
        }
    }
}
