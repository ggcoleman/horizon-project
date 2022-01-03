using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    AudioSource generalAudioSource;

    private void Awake()
    {
        generalAudioSource = GetComponent<AudioSource>();
    }

    [Header("General Sound Effects")]
    [SerializeField] AudioClip finishClip;
    [SerializeField] float finishClipVolume = 0.5f;

    [Header("Rocket Sound Effects")]
    [SerializeField] AudioClip boosterClip;
    [SerializeField] float boosterVolume = 0.5f;
    [SerializeField] AudioClip deathClip;
    [SerializeField] float deathClipVolume = 0.5f;


    [Header("Pickups")]
    [SerializeField] AudioClip fuelClip;
    [SerializeField] float fuelVolume = 0.5f;


    public void PlayTrusterAudio(AudioSource source)
    {
        PlaySound(source, boosterClip, boosterVolume, true);
    }

    public void PlayLevelFinishAudio(AudioSource source)
    {
        PlaySound(source, finishClip, finishClipVolume);
    }

    public void PlayDeathAudio(AudioSource source)
    {
        PlaySound(source, deathClip, deathClipVolume);
    }

    public void PlayFuelPickupAudio(AudioSource source)
    {
        PlaySound(source, fuelClip, fuelVolume);
    }


    public void StopSfx(AudioSource source)
    {
        source.Stop();
    }


    void PlaySound(AudioSource source, AudioClip clip, float volume, bool ensureNotAlreadyPlaying = false)
    {
        if (source != null)
        {
            if (ensureNotAlreadyPlaying)
            {
                if (!source.isPlaying)
                {
                    source.PlayOneShot(clip, volume);
                }
            }
            else
            {
                source.PlayOneShot(clip, volume);
            }

            source.loop = true;
        }
    }
}
