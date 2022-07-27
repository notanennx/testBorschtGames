using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using DG.Tweening;
using Random = UnityEngine.Random;

public class LootComponent : MonoBehaviour
{
    // Vars
    [SerializeField, BoxGroup("Main")] private int minAmount;
    [SerializeField, BoxGroup("Main")] private int maxAmount;
    [SerializeField, BoxGroup("Main")] private float lootChance;
    [SerializeField, BoxGroup("Main")] private GameObject objectToSpawn;

    // Creates loot
    public void CreateLoot()
    {
        // Exit
        if (lootChance < Random.Range(0f, 1f)) return;

        // Get
        int randomAmount = Random.Range(minAmount, maxAmount);

        // Create
        for (int i = 0; i < randomAmount; i++)
        {
            // Spawn
            GameObject newLoot = Instantiate(objectToSpawn, transform);
                newLoot.transform.parent = null;
                newLoot.transform.localScale = (0.1f * Vector3.one);
                
                // Rescale
                newLoot.transform.DOScale(Vector3.one, 0.3f);

                // Explode
                Rigidbody newRigidbody = newLoot.GetComponent<Rigidbody>();
                    newRigidbody.AddForce(new Vector3(Random.Range(-2f, 2f), Random.Range(2f, 4f), Random.Range(-2f, 2f)), ForceMode.Impulse);

                    // Rotating force
                    newRigidbody.AddForceAtPosition(
                        new Vector3(Random.Range(-2f, 2f), Random.Range(2f, 2f), Random.Range(-2f, 2f)),
                        new Vector3(Random.Range(-6f, 6f), Random.Range(2f, 4f), Random.Range(-6f, 6f)),
                        ForceMode.Impulse);
        }
    }
}
