using System;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float mainThrust = 100f;
    [SerializeField] private float rotationThrust = 1f;
    [SerializeField] private AudioClip mainEngine;

    [SerializeField] private ParticleSystem mainEngineParticles;
    [SerializeField] private ParticleSystem leftThrusterParticles;
    [SerializeField] private ParticleSystem rightThrusterParticles;

    private Rigidbody rb;
    private AudioSource audioSource;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    private void ProcessRotation()
    {
        float rotationInput = Input.GetAxis("Horizontal") * -1;

        if (rotationInput != 0f)
        {
            RotateLeftRight(rotationInput);
        }
        else
        {
            StopRotating();
        }
    }

    private void StopRotating()
    {
        leftThrusterParticles.Stop();
        rightThrusterParticles.Stop();
    }

    private void RotateLeftRight(float rotationInput)
    {
        float rotationAmount = rotationThrust * Time.deltaTime * rotationInput;
        ApplyRotation(rotationAmount);

        ParticleSystem thrusterParticles = (rotationInput < 0f) ? leftThrusterParticles : rightThrusterParticles;

        if (!thrusterParticles.isPlaying)
        {
            thrusterParticles.Play();
        }
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

    private void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);

        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }

        if (!mainEngineParticles.isPlaying)
        {
            mainEngineParticles.Play();
        }
    }

    private void StopThrusting()
    {
        audioSource.Stop();
        mainEngineParticles.Stop();
    }

    private void ApplyRotation(float rotationAmount)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationAmount);
        rb.freezeRotation = false;
    }
}
