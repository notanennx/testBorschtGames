using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class HealthComponent : MonoBehaviour
{
    // Vars
    [SerializeField, BoxGroup("Main"), ReadOnly] private int amount;
    [SerializeField, BoxGroup("Main")] private int maxAmount;
    [SerializeField, BoxGroup("Main")] private HealthbarComponent healthbar;

    // Hidden
    private DeathComponent death;

    // Awaking
    private void Awake()
    {
        // Setup
        death = GetComponent<DeathComponent>();
        amount = maxAmount;

        // Hide it
        if (healthbar)
        {
            healthbar.transform.localScale = Vector3.zero;
            UpdateHealthbar();
        }
    }

    // Updates our healthbars
    private void UpdateHealthbar()
    {
        // Exit
        if (!healthbar) return;

        // Update
        healthbar.UpdateInfo(amount.ToString(), (float)amount/maxAmount);
    }

    // Takes damages
    public void TakeDamage(int inputDamage)
    {
        // Set
        amount = Mathf.Max(0, amount - inputDamage);

        // Death
        if (amount <= 0)
        {
            if (death)
                death.Die();
            else
                Destroy(gameObject);
        }

        // Update
        UpdateHealthbar();
    }

    // Resets our health
    public void ResetHealth()
    {
        // Set
        amount = maxAmount;
        UpdateHealthbar();
    }
}
