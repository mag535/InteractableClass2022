using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverTip : MonoBehaviour
{
    public string howToInteract;
    // FIXME: could also add description and name of object

    public void OnEnable()
    {
        HoverTipManager.Instance.RegisterHoverTip(this);
    }
}
