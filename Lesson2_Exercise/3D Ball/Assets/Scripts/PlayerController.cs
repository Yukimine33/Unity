using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    PlayerCharacter player;
    FreeLookCamera freeLookCamera;
    GameMode mode;
    PlayerHUD hud;

	void Start ()
    {
        player = FindObjectOfType<PlayerCharacter>();
        freeLookCamera = FindObjectOfType<FreeLookCamera>();
        hud = FindObjectOfType<PlayerHUD>();
	}
	
	void Update ()
    {
        //获取鼠标输入
        float cameraRotateX = Input.GetAxis("Mouse X");
        float cameraRotateY = Input.GetAxis("Mouse Y");
        Vector3 rotateAngle = new Vector3(cameraRotateY, -cameraRotateX, 0);
        freeLookCamera.CameraRotate(rotateAngle);

        //获取小球平面移动输入
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 moveDirection = new Vector3(h, 0, v);
        player.Move(moveDirection);

        //获取跳跃输入
        if (Input.GetKeyDown(KeyCode.Space))
        {
            player.Jump();
        }
	}

    public void GameEnd()
    {
        hud.GameOver();
    }
}
