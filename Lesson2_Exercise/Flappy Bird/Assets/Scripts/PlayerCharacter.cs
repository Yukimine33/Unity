using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    public bool isDead = false;

    Rigidbody2D player;
    Animator animator;

    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    /// <summary>
    /// 小鸟跳跃
    /// </summary>
    public void Up()
    {
        animator.SetTrigger("Up");
        player.velocity = Vector2.zero;
        player.AddForce(new Vector2(0, 50));
    }

    /// <summary>
    /// 小鸟死亡
    /// </summary>
    void Die()
    {
        isDead = true;
        animator.SetTrigger("Die");
        player.velocity = Vector2.zero;
        GameMode.instance.GameOver();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Die();
    }
}
