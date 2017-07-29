using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeRotate : TimeBehaviour
{
    public Vector3 Direction
    {
        get { return direction; }
        set { direction = value; }
    }

    public float RotateSpeed
    {
        get { return rotateSpeed; }
        set { rotateSpeed = value; }
    }

    [SerializeField]private Vector3 direction;
    [SerializeField]private float rotateSpeed;

    public override void UpdateTime(float deltaTime)
    {
        transform.Rotate(direction.normalized, rotateSpeed * deltaTime);
    }
}
