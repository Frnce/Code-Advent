using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestScript : MonoBehaviour
{
    private LootScript lootScript;
    bool isNearChest;
    private void Awake()
    {
        lootScript = GetComponent<LootScript>();
    }
    private void Update()
    {
        if (isNearChest)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                lootScript.CalculateLoot();
                Destroy(gameObject);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isNearChest = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isNearChest = false;
        }
    }
}
