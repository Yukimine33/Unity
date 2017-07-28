using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    float height = 1;
    float distance = 3;

	void LateUpdate ()
    {
        var currentHeight = target.position.y + height;
        var currentOffSetXZ = Vector3.forward * distance;

        var pos = target.position - currentOffSetXZ;
        transform.position = new Vector3(pos.x, currentHeight, pos.z);
        transform.LookAt(target.position);
	}
}
