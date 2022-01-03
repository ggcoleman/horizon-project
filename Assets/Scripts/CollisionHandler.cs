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
    Movement movement;

    private void Awake()
    {
        movement = GetComponent<Movement>();
        audioController = FindObjectOfType<AudioController>();
    }

    private void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Friendly":
                print("Collided with 'Fiendly'");
                break;
            case "Finish":
                FinishSequence();
                break;
            case "Fuel":
                break;
            default:
                CrashSequence();
                break;
        }
    }

    private void FinishSequence()
    {
        movement.enabled = false;
        audioController.PlayLevelFinishAudio();
        StartCoroutine(LoadNextScene());
    }

    private void CrashSequence()
    {
        movement.enabled = false;
        audioController.PlayDeathAudio();
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
