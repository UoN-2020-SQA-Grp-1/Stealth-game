using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayInstruction : MonoBehaviour
{
    public Text TextUI;
    public string DisplayText;
    public GameObject Trigger;
    public enum OnTriggerEvent
    {
        SetActive,
        SetInactive
    };
    public OnTriggerEvent EventType;
    // Start is called before the first frame update
    void Start()
    {
        TextUI.gameObject.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player"))
            return;
        if (EventType == OnTriggerEvent.SetActive)
        {
            TextUI.text = DisplayText;
            TextUI.gameObject.SetActive(true);
        }
        else
            TextUI.gameObject.SetActive(false);
    }

    private void OnTriggerExit(Collider other)
    {
        TextUI.gameObject.SetActive(false);
        Destroy(Trigger);
    }
}
