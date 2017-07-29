using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewindManager : MonoBehaviour
{
    public bool isRewinding = false;

    public PlayerCharacter player;

    List<Vector3> playerPos;
    List<Quaternion> playerRot;
	
	void Update ()
    {
        var pos = player.transform.position;
        var rot = player.transform.rotation;

        if (Input.GetKey(KeyCode.R))
        {
            isRewinding = true;
        }

        if (isRewinding)
        {
            int posIndex = playerPos.Count - 1;
            int rotIndex = playerRot.Count - 1;
            player.transform.position = playerPos[posIndex];
            player.transform.rotation = playerRot[rotIndex];
            playerPos.RemoveAt(posIndex);
            playerRot.RemoveAt(rotIndex);
        }
        else
        {
            playerPos.Add(pos);
            playerRot.Add(rot);
        }
    }
}
