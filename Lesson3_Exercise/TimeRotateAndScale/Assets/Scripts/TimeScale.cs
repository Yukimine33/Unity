using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScale : TimeBehaviour
{
    public Vector3 Direction
    {
        get { return direction; }
        set { direction = value; }
    }

    public float ScaleSpeed
    {
        get { return scaleSpeed; }
        set { scaleSpeed = value; }
    }

    [SerializeField] private Vector3 direction;
    [SerializeField] private float scaleSpeed;
   
    public override void UpdateTime(float deltaTime)
    {
        transform.localScale += direction.normalized * scaleSpeed * deltaTime;
    }
}
