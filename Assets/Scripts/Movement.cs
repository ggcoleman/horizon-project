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


    [SerializeField] ParticleSystem rightThrusterParticles;
    [SerializeField] ParticleSystem leftThrusterParticles;
    [SerializeField] ParticleSystem backThrusterParticles;
    [SerializeField] ParticleSystem frontThrusterParticles;
    [SerializeField] ParticleSystem mainThrusterParticles;

    [SerializeField] Light thrusterPointLight;

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
            Rotate(force, leftThrusterParticles, rightThrusterParticles);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            Rotate(-force, rightThrusterParticles, leftThrusterParticles);
        }
        else
        {
            leftThrusterParticles.Stop();
            rightThrusterParticles.Stop();
            ApplyRotation(new Vector3());
        }
    }

    private void Rotate(Vector3 force, ParticleSystem stopThrusters, ParticleSystem startThrusters)
    {
        if (!startThrusters.isPlaying)
        {
            startThrusters.Play();
        }

        stopThrusters.Stop();
        ApplyRotation(force);
    }

    private void ApplyRotation(Vector3 force)
    {
        rb.AddTorque(force);
    }

    private void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

    private void StopThrusting()
    {
        thrusterPointLight.enabled = false;
        audioController.StopSfx(audioSource);
        backThrusterParticles.Stop();
        frontThrusterParticles.Stop();
        mainThrusterParticles.Stop();
    }

    private void StartThrusting()
    {
        var force = Vector3.up * thrustForce * Time.deltaTime;
        if (!mainThrusterParticles.isPlaying &&
        !backThrusterParticles.isPlaying &&
        !frontThrusterParticles.isPlaying)
        {
            mainThrusterParticles.Play();
            backThrusterParticles.Play();
            frontThrusterParticles.Play();
        }

        thrusterPointLight.enabled = true;
        rb.AddRelativeForce(force);
        audioController.PlayTrusterAudio(audioSource);
    }
}
