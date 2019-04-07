using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Advent.Dungeons;
using Advent.Utilities;

namespace Advent.Entities
{
    public class SelectorController : TargetingSystem
    {
        private void OnEnable()
        {
            transform.localPosition = new Vector3(0, 0, 0);
        }

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
        }
        // Update is called once per frame
        void Update()
        {
            if (gameObject.activeSelf)
            {
                RangeWeaponEquipped();
            }
        }
        public void SetIfActive(bool isActive)
        {
            if(isActive == true)
            {
                gameObject.SetActive(isActive);
            }
            if(isActive == false)
            {
                gameObject.SetActive(isActive);
            }
        }
        public void MoveSelector(int horizontal, int vertical)
        {
            if (canMove)
            {
                Vector2 start = transform.position;
                Vector2 end = start + new Vector2(horizontal, vertical);
                StartCoroutine(SmoothMovement(end));
            }
        }
        protected IEnumerator SmoothMovement(Vector3 end)
        {
            canMove = false;
            float sqrRemainingDistance = (transform.position - end).sqrMagnitude;

            while (sqrRemainingDistance > float.Epsilon)
            {
                Vector3 newPostion = Vector3.MoveTowards(rb2d.position, end, inverseMoveTime * Time.deltaTime);
                rb2d.MovePosition(newPostion);
                sqrRemainingDistance = (transform.position - end).sqrMagnitude;
                yield return null;
            }
            canMove = true;
        }
        private void RangeWeaponEquipped()
        {
            if (player.canRangeSingleAttack)
            {
                isInRange = IsInRange(player.transform.position, transform.position, player.rangeOfWeapon);
                if (isInRange)
                {
                    if (Input.GetKeyDown(KeyCode.K))
                    {
                        AttemptTarget<Enemy>(transform.position);
                    }
                }
                else
                {
                    Debug.Log("Not In range");
                }
            }
        }
        protected override void AttemptTarget<Enemy>(Vector2 selectorPosition)
        {
            base.AttemptTarget<Enemy>(selectorPosition);
            GameManager.instance.playersTurn = false;
        }
        protected override void AttackTarget<T>(T component)
        {
            Enemy hitEnemy = component as Enemy;
            if (component == hitEnemy)
            {
                hitEnemy.DamageEntity(hitEnemy.name, player.attack.GetValue());
            }
        }
        private bool IsInRange(Vector3 player, Vector3 selector, IntRange range)
        {
            float distance = Vector3.Distance(player, selector);
            if (distance <= range.m_Max)
            {
                return true;
            }
            return false;
        }
    }
}