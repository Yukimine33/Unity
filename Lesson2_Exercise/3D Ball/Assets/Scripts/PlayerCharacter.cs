using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    GameMode gameMode;
    Rigidbody player;
    PlayerController controller;
    public bool isAlive = true;
    public int coinNum;

    void Start()
    {
        player = FindObjectOfType<Rigidbody>();
        gameMode = FindObjectOfType<GameMode>();
        controller = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        if (transform.position.y < -10)
        {
            if(isAlive == true && gameMode.isGameEnd == false)
            {
                Die();
            }
        }
    }

    /// <summary>
    /// 小球平面移动
    /// </summary>
    public void Move(Vector3 direction)
    {
        player.AddForce(direction * 100 * Time.deltaTime);
    }

    /// <summary>
    /// 小球跳跃
    /// </summary>
    public void Jump()
    {
        if (Physics.Raycast(transform.position, Vector3.down, 0.51f))
        {
            player.AddForce(Vector3.up * 400);
        }
    }

    /// <summary>
    /// 吃硬币
    /// </summary>
    public void AddCoin()
    {
        coinNum += 1;
    }

    /// <summary>
    /// 判断角色是否死亡
    /// </summary>
    /// <returns></returns>
    void Die()
    {
        isAlive = false;
        gameMode.isGameEnd = true;
        controller.GameEnd();
    }
}
