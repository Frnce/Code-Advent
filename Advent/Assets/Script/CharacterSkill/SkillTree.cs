using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent.Skills
{
    [CreateAssetMenu(fileName = "New Skill tree", menuName ="Characters/SkillTree")]
    public class SkillTree : ScriptableObject
    {
        public List<Skill> allSkills = new List<Skill>();
    }

}