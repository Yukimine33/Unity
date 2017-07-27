using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFire : MonoBehaviour
{
    [SerializeField]
    private Rigidbody bullet;

    void Start()
    {
    }

    void Update()
    {
    }

    public void Fire()
    {
        Rigidbody bullet_out;
        bullet_out = Instantiate(bullet, transform.position, transform.rotation) as Rigidbody;
        bullet_out.gameObject.SetActive(true);
        bullet_out.AddForce(transform.forward * 1500);
        Destroy(bullet_out.gameObject, 2);
    }
}
