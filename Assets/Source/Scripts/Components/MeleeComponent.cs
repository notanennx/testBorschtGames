using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class MeleeComponent : MonoBehaviour
{
    // Vars
    [SerializeField, BoxGroup("Main")] private LayerMask targetLayer;
    [SerializeField, BoxGroup("Main")] private Transform currentTarget;

    // Getters
    public void ResetTarget() => currentTarget = null;
    public Transform GetTarget() => currentTarget;

    // Entered zone
    private void OnTriggerEnter(Collider other)
    {
        // Setup
        currentTarget = other.transform;
    }

    // Left trigger zone
    private void OnTriggerExit(Collider other)
    {
        // Setup
        currentTarget = null;
    }
}
