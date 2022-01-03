using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{

    [SerializeField] float loadNextSceneDelay = 2.5f;

    [SerializeField] float reloadSceneDelay = 1.5f;

    public void ReloadCurrentScene()
    {
        StartCoroutine(ReloadScene());
    }

    public void LoadTheNextScene(float? overrideLoadDelay = null)
    {

        StartCoroutine(LoadNextScene(overrideLoadDelay));
    }

    IEnumerator LoadNextScene(float? overrideLoadDelay = null)
    {
        yield return new WaitForSeconds(overrideLoadDelay ?? loadNextSceneDelay);

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
