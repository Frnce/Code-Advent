using Advent.Dungeons;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent.Entities
{
    public abstract class TargetingSystem : MonoBehaviour
    {
        public float moveTime = 0.1f;
        public LayerMask blockingLayer;
        protected Rigidbody2D rb2d;
        protected float inverseMoveTime;
        protected BoxCollider2D boxCollider;
        protected GridData gridData;
        protected Player player;

        protected bool canMove = true;
        protected bool isInRange = false;

        protected virtual void Start()
        {
            player = Player.instance;
            rb2d = GetComponent<Rigidbody2D>();
            boxCollider = GetComponent<BoxCollider2D>();
            gridData = FindObjectOfType<GridData>();
            inverseMoveTime = 1f / moveTime;
        }
        protected virtual void AttemptTarget<Enemy>(Vector2 selectorPosition) where Enemy : Component
        {
            RaycastHit2D hit;
            bool hasTarget = Target(selectorPosition, out hit);
            if (hit.transform == null)
            {
                return;
            }
            Enemy enemyComponent = hit.transform.GetComponent<Enemy>();
            if (enemyComponent != null)
            {
                AttackTarget(enemyComponent);
            }
        }
        protected abstract void AttackTarget<T>(T component) where T : Component;
        protected bool Target(Vector2 selectorPosition,out RaycastHit2D hit)
        {
            boxCollider.enabled = false;
            hit = Physics2D.Linecast(selectorPosition, selectorPosition, blockingLayer);
            boxCollider.enabled = true;
            if (hit.transform == null)
            {
                return false;
            }
            return true;
        }
    }
}