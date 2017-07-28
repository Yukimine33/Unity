using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    PlayerCharacter player;

	void Start ()
    {
        player = FindObjectOfType<PlayerCharacter>();
	}
	
	void Update ()
    {
        if(player.isDead)
        {
            return;
        }

		if(Input.GetMouseButtonDown(0))
        {
            player.Up();
        }
	}
}
