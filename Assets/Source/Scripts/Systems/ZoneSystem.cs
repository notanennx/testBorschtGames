using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class ZoneSystem : MonoBehaviour
{
    // Vars
    [SerializeField, BoxGroup("Main")] private int safeLayer;
    [SerializeField, BoxGroup("Main")] private int vulnerableLayer;

    // Actions
    public static Action OnSellzoneExit;
    public static Action OnSellzoneEnter;

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
        // Player leaves the base (also sellzone too)
        if ((inputTrigger.TryGetComponent(out PlayerComponent playerComponent)) && (inputType == EZoneType.Base))
        {
            // Dispatch
            OnSellzoneExit?.Invoke();

            // Popup hpbar
            playerComponent.GetHealthbar().Show();

            // Reset aiming
            playerComponent.GetWeapon().Show();
            playerComponent.GetAiming().SetAiming(true);
            playerComponent.gameObject.layer = vulnerableLayer;
        }
    }

    // Entered zone
    private void OnZoneEnter(EZoneType inputType, Transform inputTrigger)
    {
        // Player enters the base (also sellzone too)
        if ((inputTrigger.TryGetComponent(out PlayerComponent playerComponent)) && (inputType == EZoneType.Base))
        {
            // Dispatch
            OnSellzoneEnter?.Invoke();
        
            // Hide hpbar
            playerComponent.GetHealthbar().Hide();

            // Reset aiming
            playerComponent.GetWeapon().Hide();
            playerComponent.GetAiming().SetAiming(false);
            playerComponent.gameObject.layer = safeLayer;

            // Fuck off zombies
            foreach (EnemyComponent enemy in EnemyComponent.Hashset)
                enemy.LoseInterest(playerComponent.transform);
        }
    }
}
