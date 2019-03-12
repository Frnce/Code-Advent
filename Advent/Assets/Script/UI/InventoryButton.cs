using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Advent.UI
{
    public class InventoryButton : MonoBehaviour
    {
        [SerializeField] ShowInventory inventory;
        [SerializeField] int thisIndex = 0;
        InventorySlot inventorySlot;
        // Start is called before the first frame update
        void Start()
        {
            inventorySlot = GetComponent<InventorySlot>();
        }

        // Update is called once per frame
        void Update()
        {
            if (inventory.index == thisIndex)
            {
                GetComponent<Image>().color = Color.red;
                if (Input.GetKeyDown(KeyCode.K))
                {
                    inventorySlot.UseItem();
                }
                if (Input.GetKeyDown(KeyCode.L))
                {
                    inventorySlot.DropItem();
                }
            }
            else
            {
                GetComponent<Image>().color = Color.white;
            }
        }
    }

}