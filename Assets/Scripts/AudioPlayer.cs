using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [Header("Shooting Audio")]
    [SerializeField] AudioClip shootingClip;
    [SerializeField] [Range(0f, 1f)] float shootingVolume = 1f;

    [Header("Damage taken Audio")]
    [SerializeField] AudioClip damageTakenClip;
    [SerializeField] [Range(0f, 1f)] float damageTakenVolume = 1f;

    static AudioPlayer instance;

    /**public AudioPlayer GetInstance() //with this way of singleton, we can take the instance for playing audioclip without needing to search it, just passing the instance of it but any script can use this instance since it's a public method
    {
        return instance;
    }*/

    void Awake()
    {
        ManageSingleton();
    }

    void ManageSingleton()
    {
        // Other way to make a singleton ("more rare"), we need a static and a Getter for the instance
        int instanceCount = FindObjectsOfType(GetType()).Length;
        if(instanceCount > 1) //with the other way : if(instance != null)
        {
            gameObject.SetActive(false); //Better for stopping anything else to try to grab this gameObject before being destroyed
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void PlayShootingClip()
    {
        PlayClip(shootingClip, shootingVolume);
    }

    public void PlayDamageTakenClip()
    {
        PlayClip(damageTakenClip, damageTakenVolume);
    }

    void PlayClip(AudioClip clip, float volume)
    {
        if(clip != null)
        {
            Vector3 cameraPos = Camera.main.transform.position;
            AudioSource.PlayClipAtPoint(clip, cameraPos, volume);
        }
    }
}
