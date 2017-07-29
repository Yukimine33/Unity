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

        //通过记录一定帧数内坦克的位置和旋转角度来实现时间倒流，但有限制
        if(Input.GetKey(KeyCode.R))
        {
            player.TimeBack();
        }
        else
        {
            player.AddPosAndRot();
        }
	}
}
