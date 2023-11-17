using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventTriggerer : MonoBehaviour
{
    [SerializeField] private List<string> eventsToTriggerNames;
    [SerializeField] private UnityEvent eventsToTriggerOnStart;
    void Start()
    {
        foreach (string eventName in eventsToTriggerNames)
        {
            EventMessenger.TriggerEvent(eventName);
        }
        //foreach (UnityEvent e in eventsToTriggerOnStart)
        //{
        //    e.Invoke();
        //}
        eventsToTriggerOnStart.Invoke();
    }

    public void TriggerEvent(string name)
    {
        EventMessenger.TriggerEvent(name);
    }
}
