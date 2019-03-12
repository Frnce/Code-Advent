using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Advent.Stats;

namespace Advent.UI
{
    public class ShowHealthAndStamina : MonoBehaviour
    {
        public Text hpText;
        public Text stText;
        public Text levelText;

        StatSystem statSystem;
        LevelSystemController levelSystem;
        // Start is called before the first frame update
        void Start()
        {
            statSystem = StatSystem.instance;
            levelSystem = LevelSystemController.instance;
        }
        // Update is called once per frame
        void Update()
        {
            hpText.text = statSystem.CurrentHealth + " / " + statSystem.MaxHealth;
            stText.text = statSystem.CurrentStamina + " / " + statSystem.MaxStamina;
            levelText.text = levelSystem.currentLevel.ToString();
        }
    }
}