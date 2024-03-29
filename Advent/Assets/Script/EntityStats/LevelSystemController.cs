﻿using Advent.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent.Entities
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

        public int experienceNeeded = 250;
        public int currentExperience;

        public int PointsAddedToStats = 4;
        private void Start()
        {

        }
        //TODO add skillpoints;
        public void GainExp(int expToAdd)
        {
            currentExperience += expToAdd;

            while(currentExperience >= experienceNeeded)
            {
                //Add skillpoints
                //Add stat points

                //currentExperience -= experienceNeeded;
                //experienceNeeded *= 2;
                experienceNeeded = Mathf.RoundToInt(100 * currentLevel * Mathf.Pow(currentLevel, 0.5f));
                if(currentLevel <= maxLevel)
                {
                    currentLevel++;
                    currentExperience -= experienceNeeded;
                    AddStatPoints();
                    Debug.Log("Leveled UP! insert ff levelup music");
                }
            }
        }
        void AddStatPoints()
        {
            Player.instance.AddAvailablePoints(PointsAddedToStats);
        }
    }
}