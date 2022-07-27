using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using DG.Tweening;
using Random = UnityEngine.Random;

public class SellingSystem : MonoBehaviour
{
    // Vars
    [SerializeField, BoxGroup("Main")] private int sellPrice;

    // Hidden
    private bool isSelling;
    private float nextSell;
    private GrabComponent playerGrabber;

    // Awaking
    private void Awake()
    {
        // Set
        playerGrabber = FindObjectOfType<PlayerComponent>().GetComponentInChildren<GrabComponent>();

        // Subscribe
        ZoneSystem.OnSellzoneExit += OnSellzoneExit;
        ZoneSystem.OnSellzoneEnter += OnSellzoneEnter;
    }

    // Selling
    private void Update()
    {
        // No selling
        if (!isSelling) return;
        if (nextSell > Time.time) return;

        // Attempting
        Transform takenItem = playerGrabber.GetItem();
        if (takenItem)
        {
            // Disable
            takenItem.GetComponent<BoxCollider>().enabled = false;

            // Adds money
            MoneySystem.i.AddMoney(sellPrice);

            // Remove by tween
            takenItem.transform.DOMove(takenItem.transform.position + (16f * Vector3.up), 1.6f).OnComplete(() => {
                Destroy(takenItem.gameObject);
            });
            takenItem.transform.DOLocalRotate(new Vector3(Random.Range(0, 360f), Random.Range(0, 360f), Random.Range(0, 360f)), 1.6f);
        }

        // Cooldown
        nextSell = (Time.time + 0.15f);
    }

    // Left sellzone
    private void OnSellzoneExit()
    {
        // Set
        isSelling = false;
    }

    // Entered sellzone
    private void OnSellzoneEnter()
    {
        // Set
        isSelling = true;
    }
}
