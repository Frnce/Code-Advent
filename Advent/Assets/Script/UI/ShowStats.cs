using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Advent.Player;
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

        private PlayerController player;
        // Start is called before the first frame update
        void Start()
        {
            player = PlayerController.instance;
            panel.SetActive(isActive);
        }

        // Update is called once per frame
        void Update()
        {
            SetActiveStatBox();
            SetStatValues();
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
            attackText.text = player.physicalAttack.GetValue().ToString();
            defenseText.text = player.defense.GetValue().ToString();

            strengthText.text = player.strength.GetValue().ToString();
            dexterityText.text = player.dexterity.GetValue().ToString();
            intelligenceText.text = player.intelligence.GetValue().ToString();
            vitalityText.text = player.vitality.GetValue().ToString();
        }
    }

}