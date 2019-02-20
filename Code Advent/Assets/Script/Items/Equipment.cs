using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item
{
    public EquipmentSlot equipmentSlot;
    public int armorModifier;
    public int damageModifier;

    //temporary modifiers. just to test things out 
    //But the ideal shoud be having atleast 4 random modifiers selected from a list of all modifiers;
    //maybe make an enum or a list that has all of the modifiers available

    public int strModifier;

    public override void Use()
    {
        base.Use();
        EquipmentManager.instance.Equip(this);
        //RemoveFromInventory();
    }
}