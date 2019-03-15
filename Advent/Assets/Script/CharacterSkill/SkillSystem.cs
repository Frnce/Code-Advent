using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Advent.Skills
{
    public class SkillSystem : MonoBehaviour
    {
        #region
        public static SkillSystem instance;
        private void Awake()
        {
            instance = this;
        }
        #endregion

        public SkillTree skillTree;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}