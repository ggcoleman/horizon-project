using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheats : MonoBehaviour
{
    [SerializeField] bool enableCheats = false;

    bool collisionEnabled = true;

    SceneController sceneController;

    private void Awake()
    {
        sceneController = FindObjectOfType<SceneController>();
    }

    void Update()
    {
        if (enableCheats)
        {
            Cheat();
        }
    }

    private void Cheat()
    {
        if (Input.GetKey(KeyCode.L))
        {
            print("[Cheat] Loading the next scene...");
            sceneController.LoadTheNextScene(0.5f);
        }
        if (Input.GetKey(KeyCode.C))
        {
            collisionEnabled = !collisionEnabled;

            var capsuleCollider = GetComponent<CapsuleCollider>();
            var baseCollider = GetComponentInChildren<BoxCollider>();

            capsuleCollider.enabled = collisionEnabled;
            baseCollider.enabled = collisionEnabled;

            print("[Cheat] Colliders on: " + collisionEnabled);
        }
    }
}
