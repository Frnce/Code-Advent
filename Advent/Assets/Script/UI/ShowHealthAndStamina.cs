using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Advent.Entities;

namespace Advent.UI
{
    public class ShowHealthAndStamina : MonoBehaviour
    {
        public Text hpText;
        public Text stText;

        Player player;
        // Start is called before the first frame update
        void Start()
        {
            player = Player.instance;
        }
        // Update is called once per frame
        void Update()
        {
            hpText.text = player.currentHealth + " / " + player.maxHealth;
            stText.text = player.currentStamina + " / " + player.maxStamina;
        }
    }
}