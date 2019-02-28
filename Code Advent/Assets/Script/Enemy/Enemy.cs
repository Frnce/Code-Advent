using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Advent.EnemyStat;
using Advent.Player;

namespace Advent.Enemies
{
    public abstract class Enemy : EnemyStats, IGoap
    {
       [HideInInspector]
       public PlayerController player;

        public float stamina;
        public float regenRate;
        protected float terminalSpeed;
        protected float initialSpeed;
        protected float acceleration;
        protected float minDist = 1f;
        protected float aggroDist = 5f;
        protected bool loop = false;
        protected float maxStamina;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (stamina <= maxStamina)
            {
                Invoke("PassiveRegen", 1.0f);
            }
            else
            {
                stamina = maxStamina;
            }
        }
        public abstract void PassiveRegen();

        public HashSet<KeyValuePair<string, object>> getWorldState()
        {
            HashSet<KeyValuePair<string, object>> worldData = new HashSet<KeyValuePair<string, object>>
            {
                new KeyValuePair<string, object>("damagePlayer", false), //to-do: change player's state for world data here
                new KeyValuePair<string, object>("evadePlayer", false)
            };
            return worldData;
        }

        public void actionsFinished()
        {

        }

        public abstract HashSet<KeyValuePair<string, object>> createGoalState();

        public virtual bool moveAgent(GoapAction nextAction)
        {
            float dist = Vector3.Distance(transform.position, nextAction.target.transform.position);
            if (dist < aggroDist)
            {
                Vector3 moveDirection = player.transform.position - transform.position;

                SetSpeed(speed.GetValue());

                if (initialSpeed < terminalSpeed)
                {
                    initialSpeed += acceleration;
                }

                Vector3 newPosition = moveDirection * initialSpeed * Time.deltaTime;
                transform.position += newPosition;
            }
            if (dist <= minDist)
            {
                nextAction.SetInRange(true);
                return true;
            }
            else
            {
                return false;
            }
        }

        public void SetSpeed(float val)
        {
            terminalSpeed = val / 10;
            initialSpeed = (val / 10) / 2;
            //acceleration = (val / 10) / 4;
            acceleration = (val / 10);
            return;
        }

        public void planAborted(GoapAction aborter)
        {

        }

        public void planFailed(HashSet<KeyValuePair<string, object>> failedGoal)
        {
            
        }

        public void planFound(HashSet<KeyValuePair<string, object>> goal, Queue<GoapAction> actions)
        {

        }
    }
}