using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Advent.Stats;
using Advent.Inventories;
using Advent.Items;

namespace Advent.Player
{
    public class PlayerController : CharacterStats
    {
        #region singleton
        public static PlayerController instance;
        private void Awake()
        {
            instance = this;
        }
        #endregion
        Vector3 movement;
        Rigidbody2D rb2d;
        Animator anim;
        bool isMoving;
        bool canMove = true;
        bool isAttack = false;

        [HideInInspector]
        public bool onMenu = false;

        // Start is called before the first frame update
        public void Start()
        {
            rb2d = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();

            InitStats();

            EquipmentManager.instance.onEquipmentChanged += OnEquipmentChanged;
        }
        private void Update()
        {
            if (!onMenu)
            {
                GetInput();
            }

            if (isAttack)
            {
                StartCoroutine(CourAttack());
            }
        }
        IEnumerator CourAttack()
        {
            canMove = false;
            anim.SetTrigger("attack1");
            yield return new WaitForSeconds(0.25f);

            canMove = true;
        }
        public void GetInput()
        {
            movement = Vector3.zero;
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
            isAttack = Input.GetKeyDown(KeyCode.K);
        }
        private void FixedUpdate()
        {
            if (canMove)
            {
                if (movement != Vector3.zero)
                {
                    isMoving = true;
                    Move();
                    SetMovementAnimation();
                }
                else
                {
                    isMoving = false;
                }
            }
            else
            {
                rb2d.MovePosition(transform.position);
            }
            anim.SetBool("isMoving", isMoving);
        }
        void OnEquipmentChanged(Equipment newItem, Equipment oldItem)
        {
            if(newItem != null)
            {
                defense.AddModifier(newItem.defenseModifier);
                physicalAttack.AddModifier(newItem.pAttackModifier);
            }
            if(oldItem != null)
            {
                defense.RemoveModifier(newItem.defenseModifier);
                physicalAttack.RemoveModifier(newItem.pAttackModifier);
            }
        }
        private void Move()
        {
            rb2d.MovePosition(transform.position + movement * speed.GetValue() * Time.deltaTime);
        }
        void SetMovementAnimation()
        {
            anim.SetFloat("xMove", movement.x);
            anim.SetFloat("yMove", movement.y);
        }
    }
}