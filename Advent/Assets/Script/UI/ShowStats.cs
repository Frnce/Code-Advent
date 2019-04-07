using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Advent.Entities;
using TMPro;

namespace Advent.UI
{
    public class ShowStats : MonoBehaviour
    {
        [Space]
        [SerializeField]
        private TextMeshProUGUI attackText = null;
        [SerializeField]
        private TextMeshProUGUI defenseText = null;
        [Space]
        [SerializeField]
        private TextMeshProUGUI strengthText = null;
        [SerializeField]
        private TextMeshProUGUI dexterityText = null;
        [SerializeField]
        private TextMeshProUGUI intelligenceText = null;
        [SerializeField]
        private TextMeshProUGUI vitalityText = null;

        [SerializeField]
        private GameObject statButtonPanel = null;
        [SerializeField]
        private TextMeshProUGUI addPointsText = null;

        Player player;
        // Start is called before the first frame update
        void Start()
        {
            player = Player.instance;
        }

        // Update is called once per frame
        void Update()
        {
            SetStatValues();
            ShowAddStatButton();
        }
        void ShowAddStatButton()
        {
            if(player.GetAvailablePoints() > 0)
            {
                statButtonPanel.SetActive(true);
            }
            else
            {
                statButtonPanel.SetActive(false);
            }
        }
        void SetStatValues()
        {
            attackText.text = player.attack.GetValue().ToString();
            defenseText.text = player.defense.GetValue().ToString();

            strengthText.text = player.strength.GetValue().ToString();
            dexterityText.text = player.dexterity.GetValue().ToString();
            intelligenceText.text = player.intelligence.GetValue().ToString();
            vitalityText.text = player.vitality.GetValue().ToString();

            addPointsText.text = player.GetAvailablePoints().ToString();
        }

        public void OnPointToStr()
        {
            AddPointsToStr();
        }
        public void OnPointsToDex()
        {
            AddPointsToDex();
        }
        public void OnPointsToInt()
        {
            AddPointsToInt();
        }
        public void OnPointsToVit()
        {
            AddPointsToVit();
        }

        public void AddPointsToStr()
        {
            player.strength.AddStat(1);
            player.attack.AddStat(1);
            player.UseAvailablePoint();
        }
        public void AddPointsToDex()
        {
            player.dexterity.AddStat(1);
            player.UseAvailablePoint();
        }
        public void AddPointsToInt()
        {
            player.intelligence.AddStat(1);
            player.UseAvailablePoint();
        }
        public void AddPointsToVit()
        {
            player.vitality.AddStat(1);
            player.UseAvailablePoint();
        }
    }

}