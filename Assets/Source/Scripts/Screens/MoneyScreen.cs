using TMPro;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using NaughtyAttributes;

public class MoneyScreen : MonoBehaviour
{
    // Vars
    [SerializeField, BoxGroup("Main")] private Image moneyBacground;
    [SerializeField, BoxGroup("Main")] private TMP_Text moneyText;
    [SerializeField, BoxGroup("Main")] private string moneyFormat;

    // Hidden
    private int amount;

    // Sets amount
    public void SetAmount(int inputAmount)
    {
        // Tick
        DOTween.To(() => amount, x => amount = x, inputAmount, 0.4f).OnUpdate(() => {
            moneyText.text = string.Format(moneyFormat, amount);
        });

        // Punch
        moneyBacground.transform.DORewind();
        moneyBacground.transform.DOKill();
        moneyBacground.transform.DOPunchScale(new Vector3(0.075f, 0.075f, 0.075f), 0.4f, 4, 1f);
    }
}
