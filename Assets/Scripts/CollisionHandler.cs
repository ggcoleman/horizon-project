using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{

    [SerializeField] ParticleSystem crashParticles;

    [SerializeField] ParticleSystem finishParticles;


    AudioController audioController;
    AudioSource audioSource;
    Movement movement;

    SceneController sceneController;

    bool isTransitioning = false;

    private void Awake()
    {
        movement = GetComponent<Movement>();
        audioController = FindObjectOfType<AudioController>();
        audioSource = GetComponent<AudioSource>();
        sceneController = FindObjectOfType<SceneController>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (isTransitioning) { return; }

        switch (other.gameObject.tag)
        {
            case "Friendly":
                print("Collided with 'Fiendly'");
                break;
            case "Finish":
                FinishSequence();
                break;
            default:
                CrashSequence();
                break;
        }
    }

    private void FinishSequence()
    {
        isTransitioning = true;
        movement.enabled = false;
        finishParticles.Play();
        audioController.StopSfx(audioSource);
        audioController.PlayLevelFinishAudio(audioSource);
        sceneController.LoadTheNextScene();
    }

    private void CrashSequence()
    {
        isTransitioning = true;
        movement.enabled = false;
        crashParticles.Play();
        audioController.StopSfx(audioSource);
        audioController.PlayDeathAudio(audioSource);
        sceneController.ReloadCurrentScene();
    }


}
