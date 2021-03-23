using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    [SerializeField] float thrustSpeed = 10f;
    [SerializeField] float rotationSpeed = 5f;
    [SerializeField] float mainEngineVol = 1f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem mainBoostParticle;
    [SerializeField] ParticleSystem leftBoostParticle;
    [SerializeField] ParticleSystem rightBoosttParticle;

    Rigidbody getRigidBody;
    AudioSource getAudioSource;

    void Start()
    {
        getRigidBody = GetComponent<Rigidbody>();
        getAudioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
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

    void StartThrusting()
    {
        getRigidBody.AddRelativeForce(Vector3.up * thrustSpeed * Time.deltaTime);
        if (!getAudioSource.isPlaying)
        {
            getAudioSource.PlayOneShot(mainEngine, mainEngineVol);
        }
        if (!mainBoostParticle.isPlaying)
        {
            mainBoostParticle.Play();
        }
    }

       void StopThrusting()
    {
        getAudioSource.Stop();
        mainBoostParticle.Stop();
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            StartLeftRotation();
        }
        else  if (Input.GetKey(KeyCode.D))
        {
            StartRightRotation();
        }
        else
        {
            StopRotation();
        }
    }

    void StartLeftRotation()
    {
        ApplyRotation(rotationSpeed);
        if (!rightBoosttParticle.isPlaying)
        {
            rightBoosttParticle.Play();
        }
    }

        void StartRightRotation()
    {
        ApplyRotation(-rotationSpeed);
        if (!leftBoostParticle.isPlaying)
        {
            leftBoostParticle.Play();
        }
    }
    
    void StopRotation()
    {
        leftBoostParticle.Stop();
        rightBoosttParticle.Stop();
    }

    void ApplyRotation(float rotationThisFrame)
    {
        getRigidBody.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        getRigidBody.freezeRotation = false;
    }
}