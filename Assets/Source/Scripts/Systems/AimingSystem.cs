using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class AimingSystem : MonoBehaviour
{
    // Hidden
    private float nextReset;

    // Update
    private void Update()
    {
        foreach (AimingComponent aiming in AimingComponent.Hashset)
        {
            // Process
            aiming.FindSetTarget();
            aiming.ProcessAiming();
        }
    }
}
