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

    // Awaking
    private void Awake()
    {
        // Setup
        amount = maxAmount;

        // Hide it
        if (healthbar)
        {
            //healthbar.transform.localScale = Vector3.zero;
            UpdateHealthbar();
        }
    }

    // Updates our healthbars
    private void UpdateHealthbar() => healthbar.UpdateInfo(amount.ToString(), (float)amount/maxAmount);

    // Takes damages
    public void TakeDamage(int inputDamage)
    {
        // Set
        amount = Mathf.Max(0, amount - inputDamage);

        // Update
        UpdateHealthbar();
    }
}
