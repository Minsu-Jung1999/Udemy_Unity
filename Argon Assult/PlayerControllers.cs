using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllers : MonoBehaviour
{
    [Header("General Setup Settings")]
    [Tooltip("How fast ship moves up and down")][SerializeField] float controlSpeed = 30f;
    [SerializeField] float xRange = 10f;
    [SerializeField] float yRange = 7f;
    [SerializeField] GameObject[] lasers;

    [SerializeField] float positionPitchFacter = -2f;
    [SerializeField] float controlPitchFacter = -20f;
    [SerializeField] float positionYawFacter = 2f;
    [SerializeField] float controlRollFacter = -20;


    float xThrow, yThrow;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
        ProcessingFiring();
    }

    private void ProcessRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFacter;
        float pitchDueToControlThrow = yThrow * controlPitchFacter;

        //float yawDueToPosition = transform.localPosition.x

        float pitch = pitchDueToPosition + pitchDueToControlThrow;
        float yaw = transform.localPosition.x * positionYawFacter;
        float roll = xThrow * controlRollFacter;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    private void ProcessTranslation()
    {
        xThrow = Input.GetAxis("Horizontal");
        yThrow = Input.GetAxis("Vertical");

        float xOffset = xThrow * Time.deltaTime * controlSpeed;
        float newXPos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(newXPos, -xRange, xRange);

        float yOffset = yThrow * Time.deltaTime * controlSpeed;
        float newYPos = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(newYPos, -yRange, yRange);

        transform.localPosition = new Vector3
            (clampedXPos,
             clampedYPos,
             transform.localPosition.z);
    }

    void ProcessingFiring()
    {
        if(Input.GetButton("Fire1"))
        {
            ActiveLasers();
        }
        else
        {
            DeactivateLasers();
        }
    }

    void ActiveLasers()
    {
        LaserToggle(true);
    }

    void DeactivateLasers()
    {
        LaserToggle(false);
    }

    void LaserToggle(bool onOrOff)
    {
        foreach (GameObject laser in lasers)
        {
            var emissionMoudle = laser.GetComponent<ParticleSystem>().emission;
            emissionMoudle.enabled = onOrOff;
        }
    }


}
