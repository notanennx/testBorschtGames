using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class VisionComponent : MonoBehaviour
{
    // Vars
    [SerializeField, BoxGroup("Main")] private LayerMask targetLayer;

    // Action
    public Action<Transform> OnTargetFound;

    // Trigger
    private void OnTriggerEnter(Collider other)
    {
        // Dispatch
        OnTargetFound?.Invoke(other.transform);
    }
}
