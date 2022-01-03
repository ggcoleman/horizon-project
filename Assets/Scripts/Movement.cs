using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    AudioController audioController;

    Rigidbody rb;

    [SerializeField] float thrustForce = 100f;
    [SerializeField] float xRotationAngle = 200f;

    AudioSource audioSource;

    private void Awake()
    {
        audioController = FindObjectOfType<AudioController>();
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    private void ProcessRotation()
    {
        var force = Vector3.forward * xRotationAngle * Time.deltaTime;

        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(force);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-force);
        }
        else
        {
            ApplyRotation(new Vector3());
        }


    }

    private void ApplyRotation(Vector3 force)
    {
        rb.AddTorque(force);
    }

    private void ProcessThrust()
    {
        var force = Vector3.up * thrustForce * Time.deltaTime;

        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddRelativeForce(force);
            audioController.PlayTrusterAudio(audioSource);
        }
        else
        {
            audioController.StopSfx(audioSource);
        }
    }


}
