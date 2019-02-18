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
        
    }
    void OnEquipmentChanged(Equipment oldItem, Equipment newItem)
    {
        if(newItem != null)
        {
            attack.AddModifier(newItem.damageModifier);
        }
        
        if(oldItem != null)
        {
            attack.RemoveModifier(oldItem.damageModifier);
        }
    }
    public override void Die()
    {
        base.Die();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
