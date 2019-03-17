using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent.UI
{
    public class ShowSkillTree : MonoBehaviour
    {
        public GameObject skillTreePanel;
        bool isActive = false;
        // Start is called before the first frame update
        void Start()
        {
            skillTreePanel.SetActive(isActive);
        }

        // Update is called once per frame
        void Update()
        {
            SetActiveInventoryBox();
        }
        void SetActiveInventoryBox()
        {
            skillTreePanel.SetActive(isActive);
            if (Input.GetKeyDown(KeyCode.Y))
            {
                isActive = !isActive;
            }
        }
    }
}
