using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public OnInteract interactionMode;

    public enum OnInteract
    {
        Remove
    }

    public void TriggerMode()
    {
        Invoke(interactionMode.ToString(), 0f);
    }

    public void Remove()
    {
        Destroy(gameObject);
    }
}
