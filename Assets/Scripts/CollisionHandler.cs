using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float reloadSceneDelay = 1.5f;
    [SerializeField] float loadNextSceneDelay = 2.5f;

    AudioController audioController;
    AudioSource audioSource;
    Movement movement;

    bool isTransitioning = false;

    private void Awake()
    {
        movement = GetComponent<Movement>();
        audioController = FindObjectOfType<AudioController>();
        audioSource = GetComponent<AudioSource>();
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
        audioController.StopSfx(audioSource);
        audioController.PlayLevelFinishAudio(audioSource);
        StartCoroutine(LoadNextScene());
    }

    private void CrashSequence()
    {
        isTransitioning = true;
        movement.enabled = false;
        audioController.StopSfx(audioSource);
        audioController.PlayDeathAudio(audioSource);
        StartCoroutine(ReloadScene());
    }

    IEnumerator LoadNextScene()
    {
        yield return new WaitForSeconds(loadNextSceneDelay);

        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        var nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            SceneManager.LoadScene(nextSceneIndex);
        }

    }

    IEnumerator ReloadScene()
    {
        yield return new WaitForSeconds(reloadSceneDelay);

        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
