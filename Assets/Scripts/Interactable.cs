using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Interactable : MonoBehaviour
{
    public abstract void OnInteract();

    public void OnEnable()
    {
        InteractableManager.Instance.RegisterInteractable(this);
    }

    public void OnDisable()
    {
        InteractableManager.Instance.RemoveInteractable(this);
    }
}
