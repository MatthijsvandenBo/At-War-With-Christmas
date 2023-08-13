using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    AudioSource audioData;
    public AudioClip[] audios;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }
        audioData = GetComponent<AudioSource>();
       // audios = GetComponent <AudioClip[]>();
    }
    
    public void WalkingSound()
    {
        audioData.clip = audios[2];
        audioData.Play();
    }
    public void EndGameSound()
    {
        audioData.clip = audios[1];
        audioData.Play();
    }

    public void GunSound()
    {
        audioData.clip = audios[0];
        audioData.Play();
    }
    public void HitSound()
    {
        audioData.clip = audios[3];
        audioData.Play();
    }
}
