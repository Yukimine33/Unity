using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    protected CharacterController characterCtlr;
    protected Animator animator;
    protected bool rotateComplete = true;

    public float moveSpeed;
    public float jumpPower;
    public GameObject deathEffect;

    int health = 100;
    int damage = 100;

    Vector3 pendingVelocity;

	void Awake ()
    {
        characterCtlr = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
	}
	
	protected virtual void Update ()
    {
        pendingVelocity.z = 0f;

        characterCtlr.Move(pendingVelocity * Time.deltaTime); //角色移动

        animator.SetFloat("Speed", characterCtlr.velocity.magnitude);
        animator.SetBool("OnGround", characterCtlr.isGrounded);

        pendingVelocity.y += characterCtlr.isGrounded ? 0f : Physics.gravity.y * 10f * Time.deltaTime;

        AttackCheck();
	}

    public void AttackCheck()
    {
        var halfHeight = characterCtlr.height / 2;

        RaycastHit hit;
        if(Physics.Raycast(transform.position, Vector3.down, out hit, halfHeight + 0.05f))
        {
            if(hit.transform.GetComponent<Character>() && hit.transform != transform)
            {
                hit.transform.GetComponent<Character>().GetDamage(this, damage);
            }
        }
    }

    public void Move(float inputX)
    {
        pendingVelocity.x = inputX * moveSpeed;
    }

    public void Jump()
    {
        if(characterCtlr.isGrounded)
        {
            pendingVelocity.y = jumpPower;
        }
    }

    public void Rotate(Vector3 lookDirc,float rotateSpeed)
    {
        rotateComplete = false;

        var targetDirc = transform.position + lookDirc;
        var characterPos = transform.position;

        //去除y轴影响
        targetDirc.y = 0;
        characterPos.y = 0;
        
        Vector3 faceToDirc = targetDirc - characterPos;

        Quaternion faceToQuat = Quaternion.LookRotation(faceToDirc);

        Quaternion slerp = Quaternion.Slerp(transform.rotation, faceToQuat, rotateSpeed * Time.deltaTime);

        if(slerp == faceToQuat)
        {
            rotateComplete = true;
        }

        transform.rotation = slerp;
    }

    public void GetDamage(Character beHit, int damage)
    {
        //beHit.Jump();
        health -= damage;

        if(health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        var effect = Instantiate(deathEffect, transform.position, Quaternion.Euler(Vector3.zero));
        Destroy(effect, 2);
        Destroy(gameObject);
    }
}
