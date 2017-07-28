﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Transform target;

    public float distance = 8.0f;
    public float height = -1.0f;

	void LateUpdate ()
    {
		if(!target)
        {
            return;
        }

        transform.position = target.position;

        transform.position -= Vector3.forward * distance;

        transform.position = new Vector3(transform.position.x, height, transform.position.z);
	}
}
