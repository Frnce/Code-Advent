using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent.Skills
{
    public class Skill : ScriptableObject
    {
        public int id;
        new public string name;
        public string description;
        public Sprite icon;

        public virtual void Activate()
        {
            Debug.Log("Skill Activated");
        }
        public virtual void Deactivate()
        {
            Debug.Log("Skill Deactivated");
        }
    }
}
