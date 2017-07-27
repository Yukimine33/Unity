using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLookAt : MonoBehaviour
{
    PlayerCharacter character;
    Vector3 diff;

	void Start ()
    {
        character = FindObjectOfType<PlayerCharacter>();
	}
	
	void Update ()
    {
        diff = transform.position - character.transform.position;
        transform.position = character.transform.position + diff;
        transform.LookAt(character.transform);
	}
}
