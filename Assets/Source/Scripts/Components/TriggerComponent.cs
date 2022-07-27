using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using NaughtyAttributes;

public class TriggerComponent : MonoBehaviour
{
    // Actions
    public static Action<EZoneType, Transform> OnZoneExit;
    public static Action<EZoneType, Transform> OnZoneEnter;

    // Exited zone
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out ZoneComponent zoneComponent))
            OnZoneExit?.Invoke(zoneComponent.GetZoneType(), transform);
    }

    // Entered zone
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out ZoneComponent zoneComponent))
            OnZoneEnter?.Invoke(zoneComponent.GetZoneType(), transform);
    }
}
