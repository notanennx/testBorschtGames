using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class DeathSystem : MonoBehaviour
{
    // Vars
    [SerializeField, BoxGroup("Main")] private DeathScreen deathScreen;

    // Hidden
    private PlayerComponent currentPlayer;

    // Awaking
    private void Awake()
    {
        // Subscribe
        DeathComponent.OnDeathGlobal += OnDeath;
        deathScreen.OnRestartClicked += RevivePlayer;
    }

    // Someone just died
    private void OnDeath(DeathComponent inputDeath)
    {
        if (inputDeath.TryGetComponent(out PlayerComponent victimPlayer))
        {
            // Set
            currentPlayer = victimPlayer;

            // Hide
            victimPlayer.GetAiming().SetAiming(false);
            victimPlayer.GetHealthbar().Hide();
            victimPlayer.GetComponentInChildren<GrabComponent>().Cleanup();
            deathScreen.ShowMenu();

            // Fuck off
            foreach (EnemyComponent enemy in EnemyComponent.Hashset)
                enemy.LoseInterest(victimPlayer.transform);
        }
    }

    // Revives the player
    private void RevivePlayer()
    {
        // Exit
        if (!currentPlayer) return;

        // Revive
        deathScreen.HideMenu();
        currentPlayer.GetComponent<DeathComponent>().Respawn();
    }
}
