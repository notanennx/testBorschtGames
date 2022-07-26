using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class PatrolSystem : MonoBehaviour
{
    // Vars
    [SerializeField, BoxGroup("Main")] private Transform patrolPointsRoot;

    // Hidden
    private List<Transform> patrolPoints = new List<Transform>();

    // Awaking
    private void Awake()
    {
        // Cache patrol points
        for (int i = 0; i < patrolPointsRoot.childCount; i++)
            patrolPoints.Add(patrolPointsRoot.GetChild(i));
    }

    // Update
    private void Update()
    {
        foreach (PatrolComponent patrol in PatrolComponent.Hashset)
            patrol.AttemptPatrolling(GetRandomPatrolPosition());
    }

    // Returns a random patrol position
    private Vector3 GetRandomPatrolPosition() => patrolPoints[UnityEngine.Random.Range(0, patrolPoints.Count - 1)].position;
}
