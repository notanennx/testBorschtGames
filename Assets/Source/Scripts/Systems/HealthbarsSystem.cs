using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class HealthbarsSystem : MonoBehaviour
{
    // Hidden
    private Camera mainCamera;

    // Awaking
    private void Awake()
    {
        // Get
        mainCamera = Camera.main;
    }

    // Update health bars
    private void LateUpdate()
    {
        foreach (HealthbarComponent healthbar in HealthbarComponent.Hashset)
            healthbar.GetCanvas().transform.eulerAngles = mainCamera.transform.eulerAngles;
    }
}
