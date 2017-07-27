using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{

    public float moveSpeed;

    public float rotateSpeed;

    Rigidbody rigid;

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    void Update()
    {

    }

    public void StraightMove(float straight)
    {
        rigid.AddForce(straight * transform.forward * moveSpeed);
    }

    public void RotateMove(float round)
    {
        rigid.transform.Rotate(transform.up, round * rotateSpeed * Time.deltaTime);
    }
}
