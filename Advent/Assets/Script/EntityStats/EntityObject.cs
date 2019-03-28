using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Advent.UI;

namespace Advent.Entities
{
    public abstract class EntityObject : MonoBehaviour
    {
        public float moveTime = 0.1f;
        public LayerMask blockingLayer;
        public Entity entity;

        public int maxHealth { get; set; }
        public int currentHealth { get; set; }
        public int maxStamina { get; set; }
        public int currentStamina { get; set; }

        public Stat attack;
        public Stat defense;

        public Stat strength;
        public Stat dexterity;
        public Stat intelligence;
        public Stat vitality;

        private BoxCollider2D boxCollider;
        private Rigidbody2D rb2d;
        private float inverseMoveTime; //used to make movement more effiecient;
        private EventLogs eventLogs;

        protected LevelSystemController levelSystem;

        protected virtual void Start()
        {
            levelSystem = LevelSystemController.instance;
            boxCollider = GetComponent<BoxCollider2D>();
            rb2d = GetComponent<Rigidbody2D>();
            inverseMoveTime = 1f / moveTime;

            InitStats();

            eventLogs = EventLogs.instance;
        }
        private void InitStats()
        {
            strength.AddStat(entity.baseStr);
            dexterity.AddStat(entity.baseDex);
            intelligence.AddStat(entity.baseInt);
            vitality.AddStat(entity.baseVit);

            SetAttackStat();
            SetHP();
            SetST();

            currentHealth = maxHealth;
            currentStamina = maxStamina;
        }
        public void SetAttackStat()
        {
            attack.AddStat(strength.GetValue());
        }

        private void SetST()
        {
            maxStamina = 2 * (levelSystem.currentLevel + intelligence.GetValue());
        }

        private void SetHP()
        {
            maxHealth = 3 * (levelSystem.currentLevel + vitality.GetValue());
        }


        protected bool Move(int xDir, int yDir, out RaycastHit2D hit)
        {
            Vector2 start = transform.position;
            Vector2 end = start + new Vector2(xDir, yDir);

            boxCollider.enabled = false;
            hit = Physics2D.Linecast(start, end, blockingLayer);
            boxCollider.enabled = true;

            if (hit.transform == null)
            {
                StartCoroutine(SmoothMovement(end));
                return true;
            }
            return false;
        }
        protected IEnumerator SmoothMovement(Vector3 end)
        {
            float sqrRemainingDistance = (transform.position - end).sqrMagnitude;

            while (sqrRemainingDistance > float.Epsilon)
            {
                Vector3 newPostion = Vector3.MoveTowards(rb2d.position, end, inverseMoveTime * Time.deltaTime);
                rb2d.MovePosition(newPostion);
                sqrRemainingDistance = (transform.position - end).sqrMagnitude;
                yield return null;
            }
        }
        protected virtual void AttemptMove<T>(int xDir, int yDir) where T : Component
        {
            RaycastHit2D hit;
            bool canMove = Move(xDir, yDir, out hit);
            if (hit.transform == null)
            {
                return;
            }
            T hitComponent = hit.transform.GetComponent<T>();
            if (!canMove && hitComponent != null)
            {
                OnCantMove(hitComponent);
            }
        }
        public void DamageEntity(string beenDamaged,int damage)
        {
            ////Call the RandomizeSfx function of SoundManager to play one of two chop sounds.
            //SoundManager.instance.RandomizeSfx(chopSound1, chopSound2);

            ////Set spriteRenderer to the damaged wall sprite.
            //spriteRenderer.sprite = dmgSprite;

            ////Subtract loss from hit point total.
            //hp -= loss;

            ////If hit points are less than or equal to zero:
            //if (hp <= 0)
            //    //Disable the gameObject.
            //    gameObject.SetActive(false);
            damage = Mathf.Clamp(damage, 0, int.MaxValue); //have room for improvements ,. ,balancing shits
            currentHealth -= damage;
            eventLogs.AddEvent(beenDamaged + " has been Hit for " + damage);
            if (currentHealth <= 0)
            {
                currentHealth = 0;
                Die();
            }
        }
        protected abstract void OnCantMove<T>(T component) where T : Component;
        public virtual void Die()
        {
            //Die in some way 
            //meant to be overwritten
            Debug.Log("Died");
        }
    }
}