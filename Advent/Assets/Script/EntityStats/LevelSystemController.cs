using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent.Stats
{
    public class LevelSystemController : MonoBehaviour
    {
        public static LevelSystemController instance;
        private void Awake()
        {
            instance = this;
        }
        int availableStatPoints;

        public int maxLevel = 100;
        public int currentLevel = 1;

        public int experienceNeeded = 23;
        public int currentExperience;

        public int PointsAddedToStats = 4;
        private StatSystem statSystem;
        private void Start()
        {
            statSystem = StatSystem.instance;
        }
        //TODO add skillpoints;
        public void GainExp(int expToAdd)
        {
            currentExperience += expToAdd;

            while(currentExperience >= experienceNeeded)
            {
                //Add skillpoints
                //Add stat points

                currentExperience -= experienceNeeded;
                experienceNeeded *= 2;
                if(currentLevel <= maxLevel)
                {
                    currentLevel++;
                    AddStatPoints();
                    Debug.Log("Leveled UP! insert ff levelup music");
                }
            }
        }
        void AddStatPoints()
        {
            statSystem.availablePoints += PointsAddedToStats;
        }
    }
}