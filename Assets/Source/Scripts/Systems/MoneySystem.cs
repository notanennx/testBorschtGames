using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class MoneySystem : MonoBehaviour
{
    // Vars
    [SerializeField, BoxGroup("Main")] private MoneyScreen moneyScreen;

    // Hidden
    private int money;

    // Static
    public static MoneySystem i;

    // Awaking
    private void Awake()
    {
        // Set
        i = this;

        // Load
        SetMoney(PlayerPrefs.GetInt("money"));
    }

    // Adds money
    public void AddMoney(int inputAdd)
    {
        // Add
        SetMoney(money + inputAdd);
    }

    // Sets a money amount
    private void SetMoney(int inputAmount)
    {
        // Set
        money = Math.Max(0, inputAmount);

        // Save
        PlayerPrefs.SetInt("money", money);

        // Update
        moneyScreen.SetAmount(money);
    }
}
