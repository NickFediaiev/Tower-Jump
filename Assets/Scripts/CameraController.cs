﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public BallController target;
    private float offset;

    void Awake()
    {
        offset = transform.position.y - target.transform.position.y;   
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 currentPosition = transform.position;
        currentPosition.y = target.transform.position.y + offset;
        transform.position = currentPosition;
    }
}
