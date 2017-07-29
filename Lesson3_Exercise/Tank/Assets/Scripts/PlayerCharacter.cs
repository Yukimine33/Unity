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

    public List<Vector3> historyPoz;
    public List<Quaternion> historyRot;
    public int limit = 100;

    void Start ()
    {
        player = GetComponent<Rigidbody>();
        originalPitch = movementAudioSource.pitch;
        maxHealth = health;
        isAlive = true;
        RefreshHealthHUD();
        explosionParticles.gameObject.SetActive(false);

        historyPoz = new List<Vector3>();
        historyRot = new List<Quaternion>();
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

    /// <summary>
    /// 用于时间倒流记录坦克位置及旋转角度
    /// </summary>
    public void AddPosAndRot()
    {
        historyPoz.Add(player.position);
        historyRot.Add(player.rotation);

        if (historyPoz.Count > limit)
        {
            historyPoz.RemoveAt(0);
        }

        
        if (historyRot.Count > limit)
        {
            historyRot.RemoveAt(0);
        }
    }

    public void TimeBack()
    {
        if(historyPoz.Count > 0)
        {
            int index = historyPoz.Count - 1;
            this.transform.position = historyPoz[index];
            historyPoz.RemoveAt(index);
        }

        if (historyRot.Count > 0)
        {
            int index = historyRot.Count - 1;
            this.transform.rotation = historyRot[index];
            historyRot.RemoveAt(index);
        }
    }
}
