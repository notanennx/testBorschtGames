using TMPro;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;
using DG.Tweening;

public class HealthbarComponent : MonoBehaviour
{
    // Vars
    [SerializeField, BoxGroup("Main")] private Image fillerImage;
    [SerializeField, BoxGroup("Main")] private Canvas mainCanvas;
    [SerializeField, BoxGroup("Main")] private TMP_Text mainText;

    // Hashset
    public static HashSet<HealthbarComponent> Hashset = new HashSet<HealthbarComponent>();

    // Getters
    public Canvas GetCanvas() => mainCanvas;

    // Filling
    private void OnEnable() => Hashset.Add(this);
    private void OnDisable() => Hashset.Remove(this);

    // Awaking
    private void Awake()
    {
        // Set
        mainCanvas.worldCamera = Camera.main;
    }

    // Hides hp bar
    public void Hide()
    {
        transform.DOKill();
        transform.DOScale(Vector3.zero, 0.3f);
    }

    // Shows hp bar
    public void Show()
    {
        transform.DOKill();
        transform.DOScale(Vector3.one, 0.3f);
    }

    // Updates our info
    public void UpdateInfo(string inputString, float inputFiller)
    {
        fillerImage.DOKill();
        fillerImage.DOFillAmount(inputFiller, 0.3f);

        mainText.text = inputString;

        fillerImage.transform.parent.DOKill();
        fillerImage.transform.parent.DORewind();
        fillerImage.transform.parent.DOPunchScale((0.1f * Vector3.one), 0.4f, 6, 1);
    }
}
