using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackGround : MonoBehaviour
{
    Rigidbody2D rigid2d;

	void Start ()
    {
        rigid2d = GetComponent<Rigidbody2D>();
        rigid2d.velocity = new Vector2(-1.5f, 0); //背景前进速度
    }
	
	void Update ()
    {
        if(GameMode.instance.gameOver == true)
        {
            rigid2d.velocity = Vector2.zero;
        }
	}
}
