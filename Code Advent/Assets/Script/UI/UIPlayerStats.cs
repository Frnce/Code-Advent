using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Advent.Player;

namespace Advent.UI
{
    public class UIPlayerStats : MonoBehaviour
    {
        public Text healthText;
        public Text staminaText;
        public Text attackText;
        public Text defenseText;
        public Text speedText;
        public Text strText;
        public Text dexText;
        public Text vitText;
        public Text eneText;

        PlayerController player;
        // Start is called before the first frame update
        void Start()
        {
            player = PlayerController.instance;
            healthText.text = "";
            staminaText.text = "";
            attackText.text = "";
            defenseText.text = "";
            speedText.text = "";
            strText.text = "";
            dexText.text = "";
            vitText.text = "";
            eneText.text = "";
        }

        // Update is called once per frame
        void Update()
        {
            SetStatText();
        }

        void SetStatText()
        {
            healthText.text = player.playerStats.currentHealth.ToString();
            staminaText.text = player.playerStats.currentStamina.ToString();
            attackText.text = player.playerStats.attack.GetValue().ToString();
            defenseText.text = player.playerStats.defense.GetValue().ToString();
            speedText.text = player.playerStats.speed.GetValue().ToString();
            strText.text = player.playerStats.str.GetValue().ToString();
            dexText.text = player.playerStats.dex.GetValue().ToString();
            vitText.text = player.playerStats.vit.GetValue().ToString();
            eneText.text = player.playerStats.energy.GetValue().ToString();
        }
    }
}
