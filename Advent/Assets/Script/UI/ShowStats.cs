using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Advent.Player;
using Advent.Stats;
namespace Advent.UI
{
    public class ShowStats : MonoBehaviour
    {
        public GameObject panel;
        bool isActive = false;
        [Space]
        [SerializeField]
        private Text attackText = null;
        [SerializeField]
        private Text defenseText = null;
        [Space]
        [SerializeField]
        private Text strengthText = null;
        [SerializeField]
        private Text dexterityText = null;
        [SerializeField]
        private Text intelligenceText = null;
        [SerializeField]
        private Text vitalityText = null;

        [SerializeField]
        private GameObject statButtonPanel = null;
        [SerializeField]
        private Text addPointsText;

        private StatSystem statSystem;
        // Start is called before the first frame update
        void Start()
        {
            statSystem = StatSystem.instance;
            panel.SetActive(isActive);
        }

        // Update is called once per frame
        void Update()
        {
            SetActiveStatBox();
            SetStatValues();
            ShowAddStatButton();
        }
        void ShowAddStatButton()
        {
            if(statSystem.availablePoints > 0)
            {
                statButtonPanel.SetActive(true);
            }
            else
            {
                statButtonPanel.SetActive(false);
            }
        }
        void SetActiveStatBox()
        {
            panel.SetActive(isActive);
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                isActive = !isActive;
            }
        }
        void SetStatValues()
        {
            attackText.text = statSystem.physicalAttack.GetValue().ToString();
            defenseText.text = statSystem.defense.GetValue().ToString();

            strengthText.text = statSystem.strength.GetValue().ToString();
            dexterityText.text = statSystem.dexterity.GetValue().ToString();
            intelligenceText.text = statSystem.intelligence.GetValue().ToString();
            vitalityText.text = statSystem.vitality.GetValue().ToString();

            addPointsText.text = statSystem.availablePoints.ToString();
        }

        public void OnPointToStr()
        {
            statSystem.AddPointsToStr();
        }
        public void OnPointsToDex()
        {
            statSystem.AddPointsToDex();
        }
        public void OnPointsToInt()
        {
            statSystem.AddPointsToInt();
        }
        public void OnPointsToVit()
        {
            statSystem.AddPointsToVit();
        }
    }

}