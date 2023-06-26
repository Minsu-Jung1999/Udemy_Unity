using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponZoom : MonoBehaviour
{
    [SerializeField] Camera fpsCamera;

    [SerializeField] float zoomedOutFOV = 60f;
    [SerializeField] float zoomedInFOV = 20f;

    [SerializeField] float zoomOutSensitivity = 2f;
    [SerializeField] float zoomInSensitivity = .5f;
    
    [SerializeField] FirstPersonController firstperson;

    bool zoomedInToggle = false;

    private void OnDisable()
    {
        ZoomOut();
    }

    private void Update()
    {
        if(Input.GetMouseButton(1))
        {
            if(!zoomedInToggle)
            {
                ZoomIn();
            }
        }
        else
        {
            ZoomOut();
        }
    }

    private void ZoomOut()
    {
        zoomedInToggle = false;
        fpsCamera.fieldOfView = zoomedOutFOV;
        firstperson.RotationSpeed = zoomOutSensitivity;
    }

    private void ZoomIn()
    {
        zoomedInToggle = true;
        fpsCamera.fieldOfView = zoomedInFOV;
        firstperson.RotationSpeed = zoomInSensitivity;
    }
}
