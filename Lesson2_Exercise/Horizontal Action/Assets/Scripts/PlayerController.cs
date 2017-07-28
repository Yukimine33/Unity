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
	
	void Update ()
    {
        var h = Input.GetAxis("Horizontal");
        player.Move(h);

        if(h != 0)
        {
            var dirc = Vector3.right * h;
            player.Rotate(dirc, 10);
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            player.Jump();
        }

        if(Input.GetKeyDown(KeyCode.G))
        {
            player.GrabCheck();
        }
	}
}
