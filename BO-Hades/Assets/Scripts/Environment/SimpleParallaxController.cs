using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleParallaxController : MonoBehaviour
{
    public Transform[] backgroundLayers; // Array of background layers
    public float parallaxStrength = 0.1f; // Strength of the parallax effect

    private Transform cameraTransform;
    private Vector3 previousCameraPosition;

    private void Start()
    {
        cameraTransform = Camera.main.transform;
        previousCameraPosition = cameraTransform.position;
    }

    private void Update()
    {
        Vector3 cameraDeltaMovement = cameraTransform.position - previousCameraPosition;

        for (int i = 0; i < backgroundLayers.Length; i++)
        {
            float parallaxMovement = cameraDeltaMovement.x * parallaxStrength * (i + 1); // Multiply by (i + 1) for layer speed variation

            Vector3 backgroundTargetPosition = backgroundLayers[i].position + Vector3.right * parallaxMovement;
            backgroundLayers[i].position = backgroundTargetPosition;
        }

        previousCameraPosition = cameraTransform.position;
    }
}