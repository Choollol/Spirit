using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTriggerer : MonoBehaviour
{
    [SerializeField] private List<string> eventsToTrigger;
    void Start()
    {
        foreach (string eventName in eventsToTrigger)
        {
            EventMessenger.TriggerEvent(eventName);
        }
    }

    public void TriggerEvent(string name)
    {
        EventMessenger.TriggerEvent(name);
    }
}
