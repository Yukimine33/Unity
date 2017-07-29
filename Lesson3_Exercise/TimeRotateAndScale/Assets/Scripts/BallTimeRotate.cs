using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallTimeRotate : TimeBehaviour
{
    public float RotateSpeed
    {
        get { return rotateSpeed; }
        set { rotateSpeed = value; }
    }

    [SerializeField] private float rotateSpeed;
    [SerializeField] private Transform target;

    protected override void Awake()
    {
        target = GetComponent<Transform>();
        base.Awake();
    }

    public override void UpdateTime(float deltaTime)
    {
        transform.Rotate(target.forward, rotateSpeed * deltaTime);
    }
}
