using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [Header("General")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileLifetime = 5f;
    [SerializeField] float baseFiringRate = 0.2f;
    
    [Header("AI")]
    [SerializeField] bool useAI; //to determine if player or enemy
    [SerializeField] float firingRateVariance = 0f;
    [SerializeField] float minimumFiringRate = 0.1f;

    [HideInInspector] public bool isFiring;

    Coroutine firingCoroutine;
    AudioPlayer audioPlayer;

    void Awake() 
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();    
    }

    void Start()
    {
        if(useAI)
        {
            isFiring = true;
        }
        
    }

    void Update()
    {
        Fire();
    }

    void Fire()
    {
        if(isFiring && firingCoroutine == null)
        {
            firingCoroutine = StartCoroutine(FireContinuously());
        }
        else if(!isFiring && firingCoroutine != null)
        {
            StopCoroutine(firingCoroutine);
            firingCoroutine = null;
        }
    }

    IEnumerator FireContinuously()
    {
        while(true)
        {
            GameObject instance = Instantiate(projectilePrefab, //see EnemySpawner to undestand Instantiate call
                                            transform.position,
                                            Quaternion.identity); //Quarternion.identity = no rotation
            Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();
            if(rb != null)
            {
                rb.velocity = transform.up * projectileSpeed;
            }
            Destroy(instance, projectileLifetime);
            float TimeToNextProjectile = GetTimeToNextProjectile();
            audioPlayer.PlayShootingClip();
            //audioPlayer.GetInstance().PlayShootingClip(); we prefer to avoid this type of access since we can loose control over it
            yield return new WaitForSeconds(TimeToNextProjectile);
        }
    }

    float GetTimeToNextProjectile()
    {
        float firingRate = Random.Range(baseFiringRate - firingRateVariance,
                                        baseFiringRate + firingRateVariance);
        return Mathf.Clamp(firingRate, minimumFiringRate, float.MaxValue);
    }
}
