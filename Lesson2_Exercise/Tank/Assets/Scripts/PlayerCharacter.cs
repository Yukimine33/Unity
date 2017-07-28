using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCharacter : MonoBehaviour
{
    private Rigidbody player;

    public Rigidbody bullet;
    public Transform launcher;
    public ParticleSystem explosionParticles;

    public float moveSpeed;
    public float rotateSpeed;
    public float launchForce;

    public float health;
    float maxHealth;
    bool isAlive;

    public Slider healthSlider;
    public Image healthFillImage;
    public Color healthFull = Color.green;
    public Color healthNull = Color.red;

    public AudioSource shootAudioSource;
    public AudioSource movementAudioSource;
    private float originalPitch;
    public float pitchRange = 0.1f;
    public AudioClip engineIdlingClip;
    public AudioClip engineWorkingClip;

    void Start ()
    {
        player = GetComponent<Rigidbody>();
        originalPitch = movementAudioSource.pitch;
        maxHealth = health;
        isAlive = true;
        RefreshHealthHUD();
        explosionParticles.gameObject.SetActive(false);
	}


    public void RefreshHealthHUD()
    {
        healthSlider.value = health;
        healthFillImage.color = Color.Lerp(healthNull, healthFull, health / maxHealth);
    }

    public void Move(float verticalInput)
    {
        if(!isAlive)
        {
            return;
        }

        Vector3 dirc = verticalInput * transform.forward * moveSpeed * Time.deltaTime;
        player.MovePosition(player.position + dirc);
    }


    public void Rotate(float horizontalInput)
    {
        if (!isAlive)
        {
            return;
        }

        float turnEuler = horizontalInput * rotateSpeed * Time.deltaTime;
        Quaternion rotation = Quaternion.Euler(0, turnEuler, 0);
        player.MoveRotation(player.rotation * rotation);
    }

    public void Fire()
    {
        if (!isAlive)
        {
            return;
        }

        Rigidbody bulletOut = Instantiate(bullet, launcher.position, launcher.rotation) as Rigidbody;
        bulletOut.velocity = launchForce * launcher.forward;
        shootAudioSource.Play();
    }

    public void GetDamage(float damage)
    {
        health -= damage;
        RefreshHealthHUD();
        if(health <= 0f && isAlive)
        {
            Die();
        }
    }

    public void Die()
    {
        isAlive = false;
        explosionParticles.transform.parent = null;
        explosionParticles.gameObject.SetActive(true);
        ParticleSystem.MainModule mainModule = explosionParticles.main;
        Destroy(explosionParticles.gameObject, mainModule.duration);
        gameObject.SetActive(false);
    }

    public void EngineVoice(float verticalInput, float horizontalInput)
    {
        if(Mathf.Abs(verticalInput) < 0.1f && Mathf.Abs(horizontalInput) < 0.1f)
        {
            if(movementAudioSource.clip == engineWorkingClip)
            {
                movementAudioSource.clip = engineIdlingClip;
                movementAudioSource.pitch = Random.Range(originalPitch - pitchRange, originalPitch + pitchRange);
                movementAudioSource.Play();
            }
        }
        else
        {
            if(movementAudioSource.clip == engineIdlingClip)
            {
                movementAudioSource.clip = engineWorkingClip;
                movementAudioSource.pitch = Random.Range(originalPitch - pitchRange, originalPitch + pitchRange);
                movementAudioSource.Play();
            }
        }
    }
}
