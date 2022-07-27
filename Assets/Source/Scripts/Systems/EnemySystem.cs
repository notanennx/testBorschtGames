using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using NaughtyAttributes;

public class EnemySystem : MonoBehaviour
{
    // Hidden
    //private float nextReset;

    // Awake

    // Update
    private void Update()
    {
        // Command
        foreach (EnemyComponent enemy in EnemyComponent.Hashset)
        {
            // Attack
            if (enemy.GetMelee().GetTarget())
                enemy.Attack();

            // Destination reset
            if (enemy.GetTarget())
            {
                Vector3 direction = (enemy.GetTarget().position - enemy.transform.position).normalized;
                Vector3 targetPosition = (enemy.GetTarget().position + (direction * -0.75f));

                if (enemy.GetNavMeshAgent().enabled)
                {
                    Debug.Log(enemy.GetNavMeshAgent().pathStatus);
                    if (enemy.GetNavMeshAgent().pathStatus == NavMeshPathStatus.PathPartial)
                        enemy.LoseInterest(enemy.GetTarget());
                    else
                        enemy.GetNavMeshAgent().SetDestination(targetPosition);
                }
            }
        }       
    }
}
