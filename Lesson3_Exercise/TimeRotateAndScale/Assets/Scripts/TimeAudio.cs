using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeAudio : TimeBehaviour
{
    public AudioSource BackGroundMusic
    {
        get { return backGroundMusic; }
        set { backGroundMusic = value; }
    }

    [SerializeField] private AudioSource backGroundMusic;
    [SerializeField] private float totalTime;

    public override void UpdateTime(float deltaTime)
    {
        totalTime += deltaTime;
        backGroundMusic.Play();
    }
}
