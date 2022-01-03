using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{


    AudioSource audioSource;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    [Header("General Sound Effects")]
    [SerializeField] AudioClip finishClip;
    [SerializeField] float finishClipVolume = 0.5f;

    [Header("Rocket Sound Effects")]
    [SerializeField] AudioClip boosterClip;
    [SerializeField] float boosterVolume = 0.5f;
    [SerializeField] AudioClip deathClip;
    [SerializeField] float deathClipVolume = 0.5f;


    public void PlayTrusterAudio()
    {
        PlaySound(boosterClip, boosterVolume);
    }

    public void PlayLevelFinishAudio()
    {
        PlaySound(finishClip, finishClipVolume);
    }

    public void PlayDeathAudio()
    {
        PlaySound(deathClip, deathClipVolume);
    }


    public void StopSfx()
    {
        audioSource.Stop();
    }


    void PlaySound(AudioClip clip, float volume)
    {
        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.PlayOneShot(clip, volume);
            audioSource.loop = true;
        }
    }
}
