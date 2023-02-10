using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EvtSystem;

public class InteractableManager
{
    private static InteractableManager _instance = null;

    // Constructor 
    private InteractableManager()
    {
        EvtSystem.EventDispatcher.AddListener<PlayerInteract>(InteractTrigger);
    }


    // Only set getter to make this variable read-only
    public static InteractableManager Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = new InteractableManager();
            }

            return _instance;
        }
    }


    // Tracking Interactables in game
    private List<Interactable> _interactables = new List<Interactable>();
    
    public void RegisterInteractable(Interactable item)
    {
        _interactables.Add(item);
    }

    public void RemoveInteractable(Interactable item)
    {
        _interactables.Remove(item);
    }

    // Triggers a player-object interaction if PlayerInteract listener detects something
    private void InteractTrigger(PlayerInteract evt)
    {
        InteractWithObjects(evt.interactPosition, evt.interactDirection, 
            evt.interactDistance);
    }


    // To handle object interactions
    public void InteractWithObjects(Vector3 interactPosition, Vector3 interactDirection, 
        float interactDistance)
    {
        float closestDistance = interactDistance;
        Interactable closestInteractable = null;

        foreach(Interactable item in _interactables)
        {
            float distance = Vector3.Distance(item.transform.position, interactPosition);
            // FIXME: cos check not working
            float cosAngle = Vector3.Dot(interactDirection, 
                                   item.transform.position - interactPosition);
            if(distance < closestDistance && cosAngle >= 0.0f)
            {
                closestDistance = distance;
                closestInteractable = item;
            }
        }

        if (closestInteractable != null)
        {
            closestInteractable.OnInteract();
        }

        return;
    }
}
