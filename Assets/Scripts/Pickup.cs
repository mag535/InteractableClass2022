using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : Interactable
{
    public override void OnInteract()
    {
        OnDisable();
        Destroy(gameObject);
    }
}
