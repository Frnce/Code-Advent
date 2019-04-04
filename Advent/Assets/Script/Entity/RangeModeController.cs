using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent.Entities
{
    public class RangeModeController : MonoBehaviour
    {
        public GameObject rangeModeHolder;
        public GameObject gridIndicator;
        public GameObject indicator;

        // Start is called before the first frame update
        void Start()
        {
            //rangeModeHolder.SetActive(false);
            //indicator.SetActive(false);

            GenerateSquare(0,0,1);
        }

        // Update is called once per frame
        void Update()
        {

        }
        private void GenerateSquare(int x, int y, int radius)
        {
            for (int tileX = x - radius; tileX <= x + radius; tileX++)
            {
                for (int tileY = y - radius; tileY <= y + radius; tileY++)
                {
                    Vector3 tilePos = new Vector3(tileX + 0.5f, tileY + 0.5f, 0);
                    Instantiate(gridIndicator,tilePos, Quaternion.identity, rangeModeHolder.transform);
                }
            }
        }
    }
}