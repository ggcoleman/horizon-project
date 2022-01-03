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

    bool collidersOn = true;

    [SerializeField] ParticleSystem rightThrusterParticles;
    [SerializeField] ParticleSystem leftThrusterParticles;
    [SerializeField] ParticleSystem backThrusterParticles;
    [SerializeField] ParticleSystem frontThrusterParticles;
    [SerializeField] ParticleSystem mainThrusterParticles;

    AudioSource audioSource;

    SceneController sceneController;

    private void Awake()
    {
        audioController = FindObjectOfType<AudioController>();
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        sceneController = FindObjectOfType<SceneController>();
    }

    void Update()
    {
        ProcessThrust();
        ProcessRotation();
        Cheat();
    }

    private void Cheat()
    {
        if (Input.GetKey(KeyCode.L))
        {
            print("[Cheat] Loading the next scene...");
            sceneController.LoadTheNextScene(0f);
        }
        if (Input.GetKey(KeyCode.C))
        {
            collidersOn = !collidersOn;

            var capsuleCollider = GetComponent<CapsuleCollider>();
            var baseCollider = GetComponentInChildren<BoxCollider>();

            capsuleCollider.enabled = collidersOn;
            baseCollider.enabled = collidersOn;

            print("[Cheat] Colliders on: " + collidersOn);
        }
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

        rb.AddRelativeForce(force);
        audioController.PlayTrusterAudio(audioSource);
    }
}
