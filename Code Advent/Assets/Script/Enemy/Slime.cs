using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Advent.Player;

[RequireComponent(typeof(Rigidbody2D))]
public class Slime : MonoBehaviour
{
    public EnemyObject data;

    public Transform detectionCircle;
    public float detectionRadius;
    public LayerMask detectable;


    Rigidbody2D rb2d;
    PlayerController target;

    private int hp;
    private int attack;
    private int defense;

    LevelManager levelManager;

    bool detectedEnemy;
    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        target = PlayerController.instance;
        levelManager = FindObjectOfType<LevelManager>();
    }
    void Start()
    {
        detectedEnemy = false;

        hp = data.health;
        attack = data.attack;
        defense = data.defense;
    }

    // Update is called once per frame
    void Update()
    {
        DetectEnemy();

        IsDead();
    }
    private void FixedUpdate()
    {
        if (detectedEnemy)
        {
            Move();
        }
    }
    void Move() // move the enemy towards player
    {
        rb2d.MovePosition(Vector2.MoveTowards(transform.position, target.transform.position, data.movementSpeed * Time.deltaTime));
    }
    //If this is true - it will follow the player until the player will go out of radius;
    void DetectEnemy()
    {
        detectedEnemy = Physics2D.OverlapCircle(detectionCircle.position, detectionRadius, detectable);
    }
    public void EnemyHit(int damage)
    {
        hp -= damage;
        Debug.Log("Enemy has been Hit");
    }
    public void IsDead()
    {
        if (hp <= 0)
        {
            levelManager.enemyCount--;
            Destroy(gameObject);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(detectionCircle.position, detectionRadius);
    }
}
