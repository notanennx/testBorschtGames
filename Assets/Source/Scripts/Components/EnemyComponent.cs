using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using NaughtyAttributes;

public class EnemyComponent : MonoBehaviour
{
    // Vars
    [SerializeField, BoxGroup("Main")] private VisionComponent vision;
    [SerializeField, BoxGroup("Main"), ReadOnly] private Transform currentTarget;

    // Hidden
    private NavMeshAgent navMeshAgent;

    // Hashset
    public static HashSet<EnemyComponent> Hashset = new HashSet<EnemyComponent>();

    // Filling
    private void OnEnable() => Hashset.Add(this);
    private void OnDisable() => Hashset.Remove(this);

    // Getters
    public Transform GetTarget() => currentTarget;
    public NavMeshAgent GetNavMeshAgent() => navMeshAgent;

    // Awaking
    private void Awake()
    {
        // Get
        navMeshAgent = GetComponent<NavMeshAgent>();

        // Subscribe
        vision.OnTargetFound += OnTargetFound;
    }

    // Target was found by vision
    private void OnTargetFound(Transform inputTarget)
    {
        // Exit
        if (currentTarget) return;

        // Setup
        //if ((currentTarget) && (currentTarget.GetComponent<PlayerComponent>()))
        currentTarget = inputTarget;
    }
}
