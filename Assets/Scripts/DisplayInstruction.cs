using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Lib;
using Zenject;

public class DisplayInstruction : MonoBehaviour
{
    [Inject]
    private ITextDisplayer textDisplayer;
    public string DisplayText;
    public GameObject Trigger;
    public enum OnTriggerEvent
    {
        SetActive,
        SetInactive
    };
    public OnTriggerEvent EventType;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player"))
            return;
        if (EventType == OnTriggerEvent.SetActive)
        {
            textDisplayer.ShowText(DisplayText);
        }
        else
        {
            textDisplayer.HideText();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        textDisplayer.HideText();
        Destroy(Trigger);
    }
}
