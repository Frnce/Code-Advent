using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent.Spells
{
    public class SampleSpell : MonoBehaviour
    {
        public Spells[] sampleSpells;
        public Spells[] playerSpells;
        // Start is called before the first frame update
        void Start()
        {
            playerSpells[0].id = sampleSpells[0].id;
            playerSpells[0].name = sampleSpells[0].name;
            playerSpells[0].description = sampleSpells[0].description;
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                UseSpell(playerSpells[0].id);
            }
        }

        void UseSpell(int id)
        {
            switch (id)
            {
                case 0:
                    Debug.Log("Used Spell " + playerSpells[0].name);
                    break;
                case 1:
                    Debug.Log("Used Spell " + playerSpells[1].name);
                    break;
                case 2:
                    Debug.Log("Used Spell " + playerSpells[2].name);
                    break;
                default:
                    break;
            }
        }
    }

}