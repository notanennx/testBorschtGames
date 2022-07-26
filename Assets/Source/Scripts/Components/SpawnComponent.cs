using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using NaughtyAttributes;

public class SpawnComponent : MonoBehaviour
{
    // Hidden
    private ESpawnType spawnType;

    // Actions
    public Action<SpawnComponent> OnEnableEvent;
    public Action<SpawnComponent> OnDisableEvent;

    // Setters
    public void SetSpawnType(ESpawnType inputType) => spawnType = inputType;

    // Getters
    public ESpawnType GetSpawnType() => spawnType;

    // Manage
    private void OnEnable() => OnEnableEvent?.Invoke(this);
    private void OnDisable() => OnDisableEvent?.Invoke(this);
}
