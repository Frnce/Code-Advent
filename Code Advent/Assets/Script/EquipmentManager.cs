using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Advent.Player;

public class EquipmentManager : MonoBehaviour
{
    #region Singleton
    public static EquipmentManager instance;
    private void Awake()
    {
        instance = this;
    }
    #endregion
    Equipment[] currentEquipment;
    public delegate void OnEquipmentChanged(Equipment oldItem, Equipment newItem);
    public OnEquipmentChanged onEquipmentChanged;
    Inventory inventory;

    private void Start()
    {
        inventory = Inventory.instance;
        int numOfSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentEquipment = new Equipment[numOfSlots];
    }
    public void Equip(Equipment newItem) // Gets the equipment on the ground and put it on the equipment manager
    {
        int slotIndex = (int)newItem.equipmentSlot;

        Equipment oldItem = null;

        if(currentEquipment[slotIndex] != null)
        {
            oldItem = currentEquipment[slotIndex];
            Instantiate(oldItem.itemObject,FindObjectOfType<PlayerController>().transform.position,Quaternion.identity); // instantiate the old equipped when replaced it with the newly equipped item
        }
        if(onEquipmentChanged != null)
        {
            onEquipmentChanged.Invoke(oldItem,newItem);
            //invokes delegate to get the item data
        }
        currentEquipment[slotIndex] = newItem;
    }
    public void Unequip(int slotIndex)
    {
        if(currentEquipment[slotIndex] != null)
        {
            Equipment oldItem = currentEquipment[slotIndex];
            inventory.Add(oldItem);

            currentEquipment[slotIndex] = null;
            if (onEquipmentChanged != null)
            {
                onEquipmentChanged.Invoke(oldItem, null);
            }
        }
    }
}
