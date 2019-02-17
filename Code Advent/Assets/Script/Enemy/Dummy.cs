using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy : MonoBehaviour
{
    public EnemyObject data;


    private int hp;
    private int attack;
    private int defense;

    private void Awake()
    {
        hp = data.health;
        attack = data.attack;
        defense = data.defense;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void EnemyHit(int damage)
    {
        hp -= damage;
        Debug.Log("Enemy has been Hit for " + damage + " Current HP : " + hp);
    }
}
