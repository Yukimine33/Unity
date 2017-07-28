using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    Character AIPlayer;

    float timeSinceLastCheck = 0;

    float stimulateImputX;
    bool stimulateJump;

	void Start ()
    {
        AIPlayer = GetComponent<Character>();
	}
	
	void Update ()
    {
		if(Time.time > timeSinceLastCheck + 2)
        {
            timeSinceLastCheck = Time.time;

            stimulateImputX = Random.Range(-1f, 1f);
            stimulateJump = Random.Range(0, 2) == 1 ? true : false;
        }

        StimulateMove(stimulateImputX);
        StimulateJump(stimulateJump);
	}

    void StimulateMove(float input)
    {
        AIPlayer.Move(input);

        if (input != 0)
        {
            var dirc = Vector3.right * input;
            AIPlayer.Rotate(dirc, 10);
        }
    }

    void StimulateJump(bool jump)
    {
        if(jump)
        {
            AIPlayer.Jump();
            stimulateJump = false;
        }
    }
}
