using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeLookCamera : MonoBehaviour
{
    public Transform playerCamera;
	
	void LateUpdate ()
    {
        transform.position = playerCamera.position;
	}

    public void CameraRotate(Vector3 angle)
    {
        transform.eulerAngles -= angle;
    }
}
