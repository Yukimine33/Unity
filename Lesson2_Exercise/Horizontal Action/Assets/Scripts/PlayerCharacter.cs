using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : Character
{
    public Transform grabBox;
    public Transform grabSocket;
    public int armsAnimatorLayer;

	void Start ()
    {
		if(animator)
        {
            animator.SetLayerWeight(armsAnimatorLayer, 1f);
        }
	}
	
	protected override void Update ()
    {
        animator.SetBool("Grab", grabBox ? true : false);
        base.Update();
	}

    public void GrabCheck()
    {
        if(grabBox != null && rotateComplete)
        {
            grabBox.transform.SetParent(null);
            grabBox.GetComponent<Rigidbody>().isKinematic = false;
            grabBox = null;
        }
        else
        {
            var dist = characterCtlr.radius;

            RaycastHit hitBox;
            Debug.DrawLine(transform.position, transform.position + transform.forward * (dist + 1f), Color.blue, 5f);
            if(Physics.Raycast(transform.position, transform.forward, out hitBox, dist + 1f))
            {
                if(hitBox.collider.CompareTag("GrabBox"))
                {
                    grabBox = hitBox.transform;
                    grabBox.SetParent(grabSocket);
                    grabBox.localPosition = Vector3.zero;
                    grabBox.localRotation = Quaternion.identity;
                    grabBox.GetComponent<Rigidbody>().isKinematic = true;
                }
            }
        }
    }
}
