using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Advent.Dungeons;
using Advent.Utilities;

namespace Advent.Entities
{
    public class SelectorController : MonoBehaviour
    {
        public float moveTime = 0.1f;
        Rigidbody2D rb2d;
        private float inverseMoveTime;
        private BoxCollider2D boxCollider;
        private GridData gridData;

        bool canMove = true;
        private void OnEnable()
        {
            transform.localPosition = new Vector3(0, 0, 0);
        }
        // Start is called before the first frame update
        void Start()
        {
            rb2d = GetComponent<Rigidbody2D>();
            boxCollider = GetComponent<BoxCollider2D>();
            gridData = FindObjectOfType<GridData>();
            inverseMoveTime = 1f / moveTime;
        }
        // Update is called once per frame
        void Update()
        {
            if (gameObject.activeSelf)
            {
                if (Input.GetKeyDown(KeyCode.K))
                {
                    CheckTileHit();
                }
            }
        }
        public void MoveSelector(int horizontal,int vertical)
        {
            if (canMove)
            {
                Vector2 start = transform.position;
                Vector2 end = start + new Vector2(horizontal, vertical);
                StartCoroutine(SmoothMovement(end));
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
        private void CheckTileHit()
        {
            Vector2 start = transform.position;
            Debug.Log(gridData.GetTileStatus(start));
            if (Player.instance.canRangeSingleAttack && gridData.GetTileStatus(start) == TileStatus.ENEMY)
            {
                Debug.Log("Hit Enemy");
            }
        }
    }
}