using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Advent.Player;

public class CameraController : MonoBehaviour
{
    PlayerController player;
    Vector3 target, mousePos, refvel, shakeOffset;
    float cameraDis = 3.5f;
    float smoothTime = 0.2f, zstart;

    float shakeMag, shakeTimeEnd;
    Vector3 shakeVector;

    bool shaking;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        target = player.transform.position;
        zstart = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = CaptureMousePos();
        target = UpdateTargetPos();
        UpdateCameraPosition();
    }

    private void UpdateCameraPosition()
    {
        Vector3 tempPos;
        tempPos = Vector3.SmoothDamp(transform.position, target, ref refvel, smoothTime);
        transform.position = tempPos;
    }

    private Vector3 UpdateTargetPos()
    {
        Vector3 mouseOffset = mousePos * cameraDis;
        Vector3 ret = player.transform.position + mouseOffset;
        ret.z = zstart;
        return ret;
    }

    private Vector3 CaptureMousePos()
    {
        Vector2 ret = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        ret *= 2;
        ret -= Vector2.one;
        float max = 0.9f;
        if(Mathf.Abs(ret.x) > max || Mathf.Abs(ret.y) > max)
        {
            ret = ret.normalized;
        }
        return ret;
    }
}
