using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Advent.InventorySystem;
using Advent.Player;

namespace Advent
{
    public class CoinController : MonoBehaviour
    {
        public float minFollowModifier = 7;
        public float maxFollowModfier = 11;

        Vector2 _velocity = Vector2.zero;

        private int value = 1;
        Inventory inventory;
        PlayerController player;
        private void Start()
        {
            inventory = Inventory.instance;
            player = PlayerController.instance;
        }

        private void Update()
        {
            StartCoroutine(GoToPlayer());
        }
        IEnumerator GoToPlayer()
        {
            yield return new WaitForSeconds(0.5f);
            transform.position = Vector2.SmoothDamp(transform.position, player.transform.position, ref _velocity, Time.deltaTime * Random.Range(minFollowModifier, maxFollowModfier));
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                inventory.AddCoins(value);
                Destroy(gameObject);
            }
        }
    }
}