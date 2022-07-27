using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using NaughtyAttributes;
using DG.Tweening;

public class GrabComponent : MonoBehaviour
{
    // Vars
    [SerializeField, BoxGroup("Main")] private int capacity;
    [SerializeField, BoxGroup("Main")] private Transform stackPoint;

    // Hidden
    private float itemHeight = 0.275f;

    // Collections
    private Stack<Transform> itemsStack = new Stack<Transform>();
    private HashSet<Transform> itemsHashset = new HashSet<Transform>();

    // Triggering
    private void OnTriggerEnter(Collider other)
    {
        AddItem(other.transform);
    }

    // Cleans stack
    public void Cleanup()
    {
        // Remove
        while (itemsStack.Count > 0)
        {
            Transform item = itemsStack.Pop();
            if (item)
                Destroy(item.gameObject);
        }

        // Cleanup
        itemsStack.Clear();
        itemsHashset.Clear();
    }

    // Attempts to grab item
    public void AddItem(Transform inputTransform)
    {
        // Exit
        if (itemsHashset.Count >= capacity) return;
        if (itemsHashset.Contains(inputTransform)) return;

        // Tweening
        inputTransform.parent = stackPoint;
        inputTransform.DOKill();
        inputTransform.DOScale(Vector3.one, 0.3f);
        inputTransform.DOLocalMove(new Vector3(0, (itemHeight * itemsHashset.Count), 0), 0.3f);
        inputTransform.DOLocalRotate(Vector3.zero, 0.3f);

        // Adding it
        itemsStack.Push(inputTransform);
        itemsHashset.Add(inputTransform);

        // No physics
        inputTransform.GetComponent<Rigidbody>().isKinematic = true;
    }

    // Grabs an item from stack
    public Transform GetItem()
    {
        // Exit
        if (itemsHashset.Count <= 0) return null;

        // Find
        Transform targetItem = itemsStack.Pop();
            targetItem.parent = null;

        // Remove
        itemsHashset.Remove(targetItem);

        // Return
        return targetItem;
    }
}
