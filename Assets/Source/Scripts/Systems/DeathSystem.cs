using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class DeathSystem : MonoBehaviour
{
    // Awaking
    private void Awake()
    {
        // Subscribe
        DeathComponent.OnDeath += OnDeath;
    }

    // Someone just died
    private void OnDeath(DeathComponent inputDeath)
    {
        if (inputDeath.TryGetComponent(out PlayerComponent victimPlayer))
        {
            victimPlayer.GetHealthbar().Hide();

            // Fuck off
            foreach (EnemyComponent enemy in EnemyComponent.Hashset)
                enemy.LoseInterest(victimPlayer.transform);
        }
    }
}
