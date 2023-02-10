using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using EvtSystem;

public class HoverTipManager : MonoBehaviour
{
    private static HoverTipManager _instance = null;

    public TextMeshProUGUI tipText;
    public RectTransform tipWindow;

    // Constructor
    private HoverTipManager()
    {
        EvtSystem.EventDispatcher.AddListener<InteractTip>(TipTrigger);
    }

    // Only set getter to make this variable read-only
    public static HoverTipManager Instance
    {
        get
        {
            if (_instance == null)
            {
                // FIXME: find code that did this, but for prefabs (ie. AudioManager)
                _instance = new HoverTipManager();
            }

            return _instance;
        }
    }

    // Tracking Interactables in game
    private List<HoverTip> _hoverTips = new List<HoverTip>();

    public void RegisterHoverTip(HoverTip item)
    {
        _hoverTips.Add(item);
    }

    public void RemoveHoverTip(HoverTip item)
    {
        _hoverTips.Remove(item);
    }

    // Triggers a tooltip that tells the player how to interact with the object they are looking at
    private void TipTrigger(InteractTip evt)
    {
        ShowInteractTip(evt.interactPosition, evt.interactDirection,
            evt.interactDistance, evt.mousePosition);
    }

    // FIXME: To show how to interact with objects
    public void ShowInteractTip(Vector3 interactPosition, Vector3 interactDirection,
        float interactDistance, Vector2 mousePosition)
    {
        float closestDistance = interactDistance;
        HoverTip closestHoverTip = null;

        foreach (HoverTip item in _hoverTips)
        {
            float distance = Vector3.Distance(item.transform.position, interactPosition);
            // FIXME: cos check not working
            float cosAngle = Vector3.Dot(interactDirection,
                                   item.transform.position - interactPosition);
            if (distance < closestDistance && cosAngle >= 0.0f)
            {
                closestDistance = distance;
                closestHoverTip = item;
            }
        }

        if (closestHoverTip != null)
        {
            Debug.Log(closestHoverTip.howToInteract);
            //ShowTip(closestHoverTip.howToInteract, mousePosition);
            return;
        }

        HideTip();

        return;
    }

    private void ShowTip(string tip, Vector2 mousePosition)
    {
        tipText.text = tip;
        // Adjusts how many characters are shown per line based on the tipWindow's set width
        tipWindow.sizeDelta = new Vector2(tipText.preferredWidth > 200 ? 200 : tipText.preferredWidth, 
                                            tipText.preferredHeight);
        tipWindow.gameObject.SetActive(true);
        // FIXME: Shows tip window slightly to the right of mouse
        tipWindow.transform.position = new Vector2(mousePosition.x + tipWindow.sizeDelta.x * 2,
                                                    mousePosition.y);
    }

    private void HideTip()
    {
        tipText.text = default;
        tipWindow.gameObject.SetActive(false);
    }
}
