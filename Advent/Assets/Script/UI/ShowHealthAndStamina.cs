using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Advent.Entities;
using TMPro;

namespace Advent.UI
{
    public class ShowHealthAndStamina : MonoBehaviour
    {
        public TextMeshProUGUI levelText;
        public TextMeshProUGUI expText;
        public TextMeshProUGUI hpText;
        public TextMeshProUGUI stText;

        Player player;
        LevelSystemController levelSystem;
        // Start is called before the first frame update
        void Start()
        {
            levelSystem = LevelSystemController.instance;
            player = Player.instance;
        }
        // Update is called once per frame
        void Update()
        {
            levelText.text = levelSystem.currentLevel.ToString("00");
            expText.text = levelSystem.currentExperience + " / " + levelSystem.experienceNeeded;

            hpText.text = player.currentHealth + " / " + player.maxHealth;
            stText.text = player.currentStamina + " / " + player.maxStamina;
        }
    }
}