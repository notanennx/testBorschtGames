using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using NaughtyAttributes;

public class DeathScreen : MonoBehaviour
{
    // Vars
    [SerializeField, BoxGroup("Main")] private Image backgoundImage;
    [SerializeField, BoxGroup("Main")] private Button restartButton;

    // Hidden
    private bool isClicked;
    private Color fadeColor;
    private Vector3 defaultScale;

    // Actions
    public Action OnRestartClicked;

    // Awaking
    private void Awake()
    {
        // Set
        fadeColor = backgoundImage.color;
        backgoundImage.color = new Color(0, 0, 0, 0);

        defaultScale = restartButton.transform.localScale;
        restartButton.transform.localScale = Vector3.zero;

        // Subscribe
        restartButton.onClick.AddListener(delegate{ButtonClicked();});
    }

    // Button clicked
    private void ButtonClicked()
    {
        // Exit
        if (isClicked) return;

        // Hit
        isClicked = true;
        OnRestartClicked?.Invoke();
    }

    // Shows the menu
    public void ShowMenu()
    {
        // Set
        isClicked = false;

        // Fade
        backgoundImage.DOKill();
        backgoundImage.DOColor(fadeColor, 0.3f);

        // Button
        restartButton.transform.DOKill();
        restartButton.transform.DOScale(defaultScale, 0.3f).SetDelay(0.2f);
    }

    // Shows the menu
    public void HideMenu()
    {
        // Fade
        backgoundImage.DOKill();
        backgoundImage.DOColor(new Color(0, 0, 0, 0), 0.3f).SetDelay(0.2f);

        // Button
        restartButton.transform.DOKill();
        restartButton.transform.DOScale(Vector3.zero, 0.3f);
    }
}
