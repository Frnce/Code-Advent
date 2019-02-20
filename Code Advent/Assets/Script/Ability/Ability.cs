using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Ability : ScriptableObject
{
    public string aName = "New Ability";
    public float baseCooldown = 1f;
    public Image icon;

    public abstract void Initialize(GameObject obj);
    public abstract void TriggerAbility();
}
