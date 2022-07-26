using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[Serializable]
public class SpawnData
{
    public ESpawnType Type;

    public int MaxCount;
    public float Cooldown;
    public GameObject ObjectToSpawn;
    public Transform[] SpawnPoints;

    [HideInInspector] public float NextSpawn;
}

public class SpawnSystem : MonoBehaviour
{
    // Vars
    [SerializeField, BoxGroup("Main")] private SpawnData[] spawnData;

    // Hidden
    private Dictionary<ESpawnType, int> spawnedDictionary = new Dictionary<ESpawnType, int>();

    // Debug
    [Button("Debug")]
    private void DebugAmount()
    {
        foreach (SpawnData data in spawnData)
            Debug.Log("Amount: "+spawnedDictionary[data.Type]);
    }

    // Awake
    private void Awake()
    {
        // Initialize
        foreach (SpawnData data in spawnData)
            spawnedDictionary.Add(data.Type, 0);
    }

    // Update
    private void Update()
    {
        foreach (SpawnData data in spawnData)
            AttemptSpawn(data);
    }

    // Attempts to spawn various stuff
    private void AttemptSpawn(SpawnData inputData)
    {
        // Exit
        if (inputData.NextSpawn > Time.time) return;
        if (spawnedDictionary[inputData.Type] >= inputData.MaxCount) return;

        // Get point
        Transform randomSpawn = inputData.SpawnPoints[UnityEngine.Random.Range(0, inputData.SpawnPoints.Length - 1)];

        // Creating it
        GameObject newObject = Instantiate(inputData.ObjectToSpawn, randomSpawn.position, Quaternion.identity);
            SpawnComponent newSpawnComopnent = newObject.AddComponent(typeof(SpawnComponent)) as SpawnComponent;
                newSpawnComopnent.SetSpawnType(inputData.Type);

                // Subscribe
                newSpawnComopnent.OnEnableEvent += Add;
                newSpawnComopnent.OnDisableEvent += Deduct;

                Add(newSpawnComopnent);

        // Put on cooldown
        inputData.NextSpawn = (Time.time + inputData.Cooldown);
    }

    // Adds or deducts amount
    private void Add(SpawnComponent inputSpawn) => spawnedDictionary[inputSpawn.GetSpawnType()] += 1;
    private void Deduct(SpawnComponent inputSpawn) => spawnedDictionary[inputSpawn.GetSpawnType()] -= 1;
}
