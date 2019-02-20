using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : CharacterStats
{
    // Start is called before the first frame update
    void Start()
    {
        EquipmentManager.instance.onEquipmentChanged += OnEquipmentChanged;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("Player Attacks for " + (str.GetValue() + attack.GetValue()));
        }
    }
    void OnEquipmentChanged(Equipment oldItem, Equipment newItem)
    {
        if(newItem != null)
        {
            // loop out all the modifiers to add to the current modifier;
            //TEMPPORRARATTY TO TEST THIGNS
            attack.AddModifier(newItem.damageModifier);
            str.AddModifier(newItem.strModifier);
        }
        
        if(oldItem != null)
        {
            attack.RemoveModifier(oldItem.damageModifier);
            str.RemoveModifier(newItem.strModifier);
        }
    }
    public override void Die()
    {
        base.Die();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
