using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    PlayerCharacter character;
    CharacterFire bullet;
    PlayerHUD reset;

    void Start()
    {
        character = FindObjectOfType<PlayerCharacter>();
        bullet = FindObjectOfType<CharacterFire>();
    }

    void Update()
    {
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");

        character.StraightMove(v);
        character.RotateMove(h);

        if (Input.GetMouseButtonDown(0))
        {
            bullet.Fire();
        }
        reset.OnClickButton();
    }
}
