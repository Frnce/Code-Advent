using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent.UI
{
    public class InventoryButtonController : MonoBehaviour
    {
        public int index;
        [SerializeField] bool keyDown;
        int maxIndex;
        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetAxisRaw("Vertical") != 0)
            {
                if (!keyDown)
                {
                    if (Input.GetAxisRaw("Vertical") < 0)
                    {
                        if (index < maxIndex)
                        {
                            index++;
                        }
                        else
                        {
                            index = 0;
                        }
                    }
                    else if (Input.GetAxisRaw("Vertical") > 0)
                    {
                        if (index > 0)
                        {
                            index--;
                        }
                        else
                        {
                            index = maxIndex;
                        }
                    }
                    keyDown = true;
                }
            }
            else
            {
                keyDown = false;
            }
        }
    }
}