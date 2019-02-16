using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public Stat health;
    public int currentHealth { get; private set; }

    public Stat energy;
    public Stat attack;
    public Stat defense;
    public Stat speed;

    private void Awake()
    {
        currentHealth = health.GetValue();
    }
    public void TakeDamage(int damage)
    {
        damage -= defense.GetValue();
        damage = Mathf.Clamp(damage, 0, int.MaxValue); //have room for improvements ,. ,balancing shits

        currentHealth -= damage;
        Debug.Log("Take Daamge" + transform.name);

        if(currentHealth <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        //Die in some way 
        //meant to be overwritten
        Debug.Log("Died");
    }
}
