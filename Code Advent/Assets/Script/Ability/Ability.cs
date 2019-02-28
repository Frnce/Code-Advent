using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Advent.Abilities
{
    public abstract class Ability : ScriptableObject
    {
        public string aName = "New Ability";
        public float baseCooldown = 1f;
        public float staminaCost; //TODO Implement this 
        public Sprite icon;

        public abstract void Initialize(GameObject obj);
        public abstract void TriggerAbility();
    }
}