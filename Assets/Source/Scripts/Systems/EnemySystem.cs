using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class EnemySystem : MonoBehaviour
{
    // Hidden
    private float nextReset;

    // Update
    private void Update()
    {
        // Exit
        if (nextReset >= Time.time) return;

        // Command
        foreach (EnemyComponent enemy in EnemyComponent.Hashset)
        {
            if (enemy.GetTarget())
                enemy.GetNavMeshAgent().SetDestination(enemy.GetTarget().position);
        }       

        // Cooldown
        nextReset = (Time.time + 0.1f);
    }
}
