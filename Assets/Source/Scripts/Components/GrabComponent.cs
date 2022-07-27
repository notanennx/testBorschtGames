using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using NaughtyAttributes;

public class GrabComponent : MonoBehaviour
{
    // Vars
    [SerializeField, BoxGroup("Main")] private int capacity;
    [SerializeField, BoxGroup("Main")] private Transform stackPoint;

    // Hidden
    //private float itemHeight;

    // Collections
    private Stack<Transform> itemsStack = new Stack<Transform>();
    private HashSet<Transform> itemsHashset = new HashSet<Transform>();

    // Triggering
    private void OnTriggerEnter(Collider other)
    {
        AddItem(other.transform);
    }

    // Attempts to grab item
    private void AddItem(Transform inputTransform)
    {
        // Exit
        if (itemsHashset.Count >= capacity) return;
        if (itemsHashset.Contains(inputTransform)) return;

        // Adding
        itemsStack.Push(inputTransform);
        itemsHashset.Add(inputTransform);

        // Parent
        inputTransform.parent = stackPoint;
    }

    // Grabs an item from stack
    private Transform GetItem()
    {
        // Exit
        if (itemsHashset.Count <= 0) return null;

        // Find
        Transform targetItem = itemsStack.Pop();
            targetItem.parent = null;

        // Return
        return targetItem;
    }
}
