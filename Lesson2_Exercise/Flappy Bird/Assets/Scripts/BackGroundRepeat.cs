using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundRepeat : MonoBehaviour
{
    BoxCollider2D backGroundColider;
    float groundLength;

	void Start ()
    {
        backGroundColider = GetComponent<BoxCollider2D>();
        groundLength = backGroundColider.size.x;
	}
	
	void Update ()
    {
		if(transform.position.x < -groundLength)
        {
            Vector2 offset = new Vector2(groundLength * 2.0f - 0.1f, 0);
            transform.position = (Vector2)transform.position + offset;
        }
	}
}
