using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuel : MonoBehaviour
{

    AudioSource audioSource;
    AudioController audioController;
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioController = FindObjectOfType<AudioController>();
    }

    void Update()
    {
        gameObject.transform.Rotate(new Vector3(0.1f, 0.1f, 0.1f), Space.Self);
    }


    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "Player":
                FuelSequence(other);
                break;
        }
    }


    private void FuelSequence(Collider other)
    {
        audioController.PlayFuelPickupAudio(audioSource);

        GetComponent<Renderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
        var particleSystem = GetComponent<ParticleSystem>();
        particleSystem.Stop();
        particleSystem.Clear();

        StartCoroutine(DestroyFuelPickup());

    }

    IEnumerator DestroyFuelPickup()
    {
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
