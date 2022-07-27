using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class ZoneSystem : MonoBehaviour
{
    // Awake
    private void Awake()
    {
        // Subscribe
        TriggerComponent.OnZoneExit += OnZoneExit;
        TriggerComponent.OnZoneEnter += OnZoneEnter;
    }

    // Exited zone
    private void OnZoneExit(EZoneType inputType, Transform inputTrigger)
    {
        // Player leaves the base
        if ((inputTrigger.TryGetComponent(out PlayerComponent playerComponent)) && (inputType == EZoneType.Base))
        {
            // Popup hpbar
            playerComponent.GetHealthbar().Show();

            // Reset aiming
            playerComponent.GetAiming().SetAiming(true);
        }
    }

    // Entered zone
    private void OnZoneEnter(EZoneType inputType, Transform inputTrigger)
    {
        // Player enters the base
        if ((inputTrigger.TryGetComponent(out PlayerComponent playerComponent)) && (inputType == EZoneType.Base))
        {
            // Hide hpbar
            playerComponent.GetHealthbar().Hide();

            // Reset aiming
            playerComponent.GetAiming().SetAiming(false);

            // Fuck off zombies
            foreach (EnemyComponent enemy in EnemyComponent.Hashset)
                enemy.LoseInterest(playerComponent.transform);
        }
    }
}