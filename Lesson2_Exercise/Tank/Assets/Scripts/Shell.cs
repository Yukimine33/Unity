using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{
    public float existTime = 2f;
    public float explosionRadius;
    public float explosionForce = 300f;
    public float explosionDamage = 50f;
    public LayerMask damageMask;

    public ParticleSystem explosionParticle;

    public AudioSource explosionAudioSource;

	void Start ()
    {
        Destroy(gameObject, existTime);
	}

    private void OnTriggerEnter(Collider other)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius, damageMask);

        foreach(var collider in colliders)
        {
            Rigidbody targetRigid = collider.GetComponent<Rigidbody>();
            if(!targetRigid)
            {
                continue;
            }

            targetRigid.AddExplosionForce(explosionDamage, transform.position, explosionRadius);

            var targetCharacter = targetRigid.GetComponent<PlayerCharacter>();
            if(targetCharacter)
            {
                targetCharacter.GetDamage(CalculateDamage(targetRigid.position));
            }
        }
        
        explosionParticle.transform.parent = null;
        explosionParticle.Play();
        explosionAudioSource.Play();

        ParticleSystem.MainModule mainModule = explosionParticle.main;
        Destroy(explosionParticle.gameObject, mainModule.duration);
        Destroy(gameObject);
        
    }

    //伤害衰减
    float CalculateDamage(Vector3 targetPosition)
    {
        var distance = (targetPosition - transform.position).magnitude;
        var damageModify = (explosionRadius - distance) / explosionRadius;
        var damage = damageModify * explosionDamage;
        return Mathf.Max(3f, damage);
    }
}
