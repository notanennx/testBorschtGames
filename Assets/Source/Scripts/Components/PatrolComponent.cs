using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using NaughtyAttributes;

public class PatrolComponent : MonoBehaviour
{
    // Vars
    [SerializeField, BoxGroup("Main")] private bool isPatrolling = true;

    // Hidden
    private float nextPatrol;
    private NavMeshAgent navMeshAgent;

    // Hashset
    public static HashSet<PatrolComponent> Hashset = new HashSet<PatrolComponent>();

    // Awaking
    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Filling
    private void OnEnable() => Hashset.Add(this);
    private void OnDisable() => Hashset.Remove(this);

    // Getters
    public bool IsPatrolling() => isPatrolling;
    public NavMeshAgent GetNavMeshAgent() => navMeshAgent;

    // Setters
    public void SetPatrolling(bool inputPatrolling) => isPatrolling = inputPatrolling;

    // Attempts to patrol position
    public void AttemptPatrolling(Vector3 inputPosition)
    {
        // Exit
        if ((nextPatrol >= Time.time) || (!isPatrolling)) return;

        // Moving
        navMeshAgent.SetDestination(inputPosition);

        // Cooldown
        nextPatrol = (Time.time + UnityEngine.Random.Range(2f, 4f));
    }
}
