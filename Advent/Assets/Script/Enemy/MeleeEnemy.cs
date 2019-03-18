using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Advent.Stats;
using Advent.Player;

namespace Advent.Enemies
{
    public abstract class MeleeEnemy : CharacterStats , IGOAP
    {

        Rigidbody2D rb2d;
        [HideInInspector]
        public Animator anim;
        [Header("For GOAP AI")]
        public float maxStamina;
        public float stamina;
        protected float terminalSpeed;
        protected float initialSpeed;
        protected float acceleration;
        protected float minDist = 1f;
        protected float aggroDist = 5f;
        protected bool loop = false;
        protected float regenRate;

        protected Vector3 entityPosition;

        [HideInInspector]
        public PlayerController player;
        [HideInInspector]
        public StatSystem statSystem;

        public virtual void Start()
        {
            rb2d = GetComponent<Rigidbody2D>();
            player = PlayerController.instance;
            statSystem = StatSystem.instance;
            InitStats();
            stamina = maxStamina;
        }
        public virtual void Update()
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
            HashSet<KeyValuePair<string, object>> worldData = new HashSet<KeyValuePair<string, object>>();
            worldData.Add(new KeyValuePair<string, object>("damagePlayer", false)); //to-do: change player's state for world data here
            worldData.Add(new KeyValuePair<string, object>("evadePlayer", false));
            return worldData;
        }

        public abstract HashSet<KeyValuePair<string, object>> createGoalState();

        public void planFailed(HashSet<KeyValuePair<string, object>> failedGoal)
        {

        }

        public void planFound(HashSet<KeyValuePair<string, object>> goal, Queue<GOAPAction> actions)
        {

        }

        public void actionsFinished()
        {

        }

        public void planAborted(GOAPAction aborter)
        {

        }

        public void SetSpeed(float val)
        {
            terminalSpeed = val / 10;
            initialSpeed = (val / 10) / 2;
            acceleration = (val / 10) / 4;
            return;
        }

        public virtual bool moveAgent(GOAPAction nextAction)
        {
            float dist = Vector3.Distance(transform.position, nextAction.target.transform.position);
            if (dist < aggroDist)
            {
                anim.SetBool("isMoving", true);
                Vector3 moveDirection = player.transform.position - transform.position;

                SetSpeed(speed.GetValue());

                if (initialSpeed < terminalSpeed)
                {
                    initialSpeed += acceleration;
                }

                entityPosition = moveDirection * initialSpeed * Time.deltaTime;
                transform.position += entityPosition;
                anim.SetFloat("xMove", entityPosition.normalized.x);
                anim.SetFloat("yMove", entityPosition.normalized.y);
            }
            if (dist <= minDist)
            {
                nextAction.SetInRange(true);
                return true;
            }
            else
            {
                anim.SetBool("isMoving", false);
                return false;
            }
        }
    }
}
