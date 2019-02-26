using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Advent.Player;
namespace Advent.Abilities
{
    public class AbilityCooldown : MonoBehaviour
    {
        public string AbilityButtonAxisName = "Fire1"; //Button from input manager
        public Image iconImage;
        public Image darkMask;
        public Text cooldownTextDisplay;
        public PlayerController player;

        [SerializeField] private Ability ability;

        //button image
        // audio source
        private float cooldownDuration;
        private float nextReadyTime; // for next ability can
        private float cooldownTimeLeft; // for UI
                                        // Start is called before the first frame update
        void Start()
        {
            player = PlayerController.instance;
            Initialize(ability); // INitialize method will be initialized on player class select
            iconImage.sprite = ability.icon;
        }

        public void Initialize(Ability selectedAbility)
        {
            ability = selectedAbility;
            //MybuttonImage = getcomponent<image>();
            //abilitySource = getcompnonent<audiosource>();
            //mybuttonimage.sprite = ability.asprite;
            //darkMask.sprite = ability.asprite;

            cooldownDuration = ability.baseCooldown;
            ability.Initialize(player.gameObject);
        }

        // Update is called once per frame
        void Update()
        {
            bool coolDownComplete = (Time.time > nextReadyTime);
            if (coolDownComplete)
            {
                AbilityReady();
                if (Input.GetButtonDown(AbilityButtonAxisName))
                {
                    ButtonTriggered();
                }
            }
            else
            {
                Cooldown();
            }
        }
        private void AbilityReady()
        {
            cooldownTextDisplay.enabled = false;
            darkMask.enabled = false;
        }
        private void Cooldown()
        {
            cooldownTimeLeft -= Time.deltaTime;
            cooldownTextDisplay.text = cooldownTimeLeft.ToString("0.0");

            darkMask.fillAmount = (cooldownTimeLeft / cooldownDuration);
        }

        private void ButtonTriggered()
        {
            nextReadyTime = cooldownDuration + Time.time;
            cooldownTimeLeft = cooldownDuration;
            darkMask.enabled = true;
            cooldownTextDisplay.enabled = true;

            ability.TriggerAbility();
        }
    }

}