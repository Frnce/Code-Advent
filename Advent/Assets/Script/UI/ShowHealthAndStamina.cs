using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Advent.Player;

namespace Advent.UI
{
    public class ShowHealthAndStamina : MonoBehaviour
    {
        public Text hpText;
        public Text stText;

        PlayerController player;
        // Start is called before the first frame update
        void Start()
        {
            player = PlayerController.instance;
        }
        // Update is called once per frame
        void Update()
        {
            hpText.text = player.CurrentHealth + " / " + player.MaxHealth;
            stText.text = player.CurrentStamina + " / " + player.MaxStamina;
        }
    }
}