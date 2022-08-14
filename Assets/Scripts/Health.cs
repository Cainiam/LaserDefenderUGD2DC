using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Health Management")]
    [SerializeField] int health = 50;

    //Teacher advise: created a dedicated script for particle effet if there are several particle effects
    [Header("Particle effect for damage")]
    [SerializeField] ParticleSystem hitEffect;

    [Header("Camera shake management")]
    [SerializeField] bool applyCameraShake;
    CameraShake cameraShake;

    void Awake()
    {
        cameraShake = Camera.main.GetComponent<CameraShake>();
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        DamageDealer damageDealer = other.GetComponent<DamageDealer>();

        if(damageDealer != null)
        {
            TakeDamage(damageDealer.GetDamage());
            PlayHitEffect();
            ShakeCamera();
            damageDealer.Hit();
        }
    }

    void TakeDamage(int damage)
    {
        health -= damage;
        if(health <= 0 )
        {
            Destroy(gameObject);
        }
    }

    void PlayHitEffect()
    {
        if(hitEffect != null)
        {
                                                                                    //no rotation
            ParticleSystem instance = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
        }
    }

    //Advice for the teacher to follow for using a Camera Shake (more robust than ours) : https://www.youtube.com/watch?v=tu-Qe66AvtY
    void ShakeCamera()
    {
        if (cameraShake != null && applyCameraShake)
        {
            cameraShake.play();
        }
    }
}
