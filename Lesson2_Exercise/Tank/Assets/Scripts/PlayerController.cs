using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    PlayerCharacter player;

	void Start ()
    {
        player = GetComponent<PlayerCharacter>();
	}
	
	void FixedUpdate ()
    {
        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");

        player.Move(v);
        player.Rotate(h);
        player.EngineVoice(v, h);

        if(Input.GetButtonDown("Fire1"))
        {
            player.Fire();
        }
	}
}
