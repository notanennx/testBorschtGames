using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using NaughtyAttributes;

public class ZoneComponent : MonoBehaviour
{
    // Vars
    [SerializeField, BoxGroup("Main")] private EZoneType zoneType;

    // Getters
    public EZoneType GetZoneType() => zoneType;
}
