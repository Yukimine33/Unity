using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeLight : TimeBehaviour
{
    Light spotlight;

    protected override void Awake()
    {
        base.Awake();
        spotlight = GetComponent<Light>();
    }

    public float OffTime
    {
        get { return offTime; }
        set { offTime = value; }
    }

    [SerializeField] private float offTime;
    [SerializeField] private float totalTime;

    public override void UpdateTime(float deltaTime)
    {
        totalTime += deltaTime;

        if(totalTime > offTime)
        {
            spotlight.gameObject.SetActive(false);
        }
        else
        {
            spotlight.gameObject.SetActive(true);
        }
    }
}
