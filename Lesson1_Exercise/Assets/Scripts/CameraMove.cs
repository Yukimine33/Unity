using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    [SerializeField]
    private float height;

    [SerializeField]
    private float distance;

    [SerializeField]
    private float rotateDamp;

    void Start ()
    {
	}
	
	void LateUpdate ()
    {
        var currentHeight = target.position.y + height;
        var targetRotateY = target.eulerAngles.y;
        var currentRotateY = transform.eulerAngles.y;

        currentRotateY = Mathf.LerpAngle(currentRotateY, targetRotateY, rotateDamp * Time.deltaTime);
        var currentRotation = Quaternion.Euler(0, currentRotateY, 0);

        var currentOffsetXZ = currentRotation * Vector3.forward * distance;

        var currentPos = target.position - currentOffsetXZ;
        transform.position = new Vector3(currentPos.x, currentHeight, currentPos.z);
        transform.LookAt(target.position);
    }
}
