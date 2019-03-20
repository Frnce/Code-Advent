using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent.Entities
{
    public abstract class EntityObject : MonoBehaviour
    {
        public float moveTime = 0.1f;
        public LayerMask blockingLayer;

        private BoxCollider2D boxCollider;
        private Rigidbody2D rb2d;
        private float inverseMoveTime; //used to make movement more effiecient;


        public int MaxHealth;
        public int CurrentHealth { get; set; }

        public Stat attack;

        protected virtual void Start()
        {
            boxCollider = GetComponent<BoxCollider2D>();
            rb2d = GetComponent<Rigidbody2D>();
            inverseMoveTime = 1f / moveTime;

            CurrentHealth = MaxHealth;
        }

        protected bool Move(int xDir,int yDir, out RaycastHit2D hit)
        {
            Vector2 start = transform.position;
            Vector2 end = start + new Vector2(xDir, yDir);

            boxCollider.enabled = false;
            hit = Physics2D.Linecast(start, end, blockingLayer);
            boxCollider.enabled = true;

            if(hit.transform == null)
            {
                StartCoroutine(SmoothMovement(end));
                return true;
            }
            return false;
        }
        protected IEnumerator SmoothMovement(Vector3 end)
        {
            float sqrRemainingDistance = (transform.position - end).sqrMagnitude;

            while(sqrRemainingDistance > float.Epsilon)
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
            if(hit.transform == null)
            {
                return;
            }
            T hitComponent = hit.transform.GetComponent<T>();

            if(!canMove && hitComponent != null)
            {
                OnCantMove(hitComponent);
            }
        }
        public void DamageEntity(int damage)
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
            CurrentHealth -= damage;
            Debug.Log("Take Damage for " + damage);

            if (CurrentHealth <= 0)
            {
                Die();
            }
            Debug.Log("Hit " + gameObject);
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