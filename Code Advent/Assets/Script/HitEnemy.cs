using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Advent.Player;

public class HitEnemy : MonoBehaviour
{
    PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Slime>().EnemyHit(player.attack);
        }
    }
}
