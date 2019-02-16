using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitRoom : MonoBehaviour
{
    bool isNearDoor = false;
    // Update is called once per frame
    void Update()
    {
        if (isNearDoor)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                SceneManager.LoadScene("SampleScene");
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isNearDoor = true;
        }
    }
}
