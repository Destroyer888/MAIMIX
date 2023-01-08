using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class EventOneArg: UnityEvent<object>
{ }
public class EventManager : MonoBehaviour
{
    private Dictionary<string, EventOneArg> event_dictionary;
    private static EventManager event_manager;

    public static EventManager instance
    {
        get
        {
            if (!event_manager)
            {
                event_manager = FindObjectOfType(typeof(EventManager)) as EventManager;

                if (!event_manager)
                {
                    Debug.LogError("There needs to be one active EventManger script on a GameObject in your scene.");
                }
                else
                {
                    event_manager.Init();
                }
            }
            return event_manager;
        }
    }

    void Init()
    {
        if (event_dictionary == null)
        {
            event_dictionary = new Dictionary<string, EventOneArg>();
        }
    }

    public static void StartListening(string eventName, UnityAction<object> listener)
    {

        EventOneArg this_event = null;
        if (instance.event_dictionary.TryGetValue(eventName, out this_event))
        {
            this_event.AddListener(listener);
        }
        else
        {
            this_event = new EventOneArg();
            this_event.AddListener(listener);
            instance.event_dictionary.Add(eventName, this_event);
        }
    }
    public static void StopListening(string eventName, UnityAction<object> listener)
    {
        if (instance == null) return;
        EventOneArg this_event = null;
        if (instance.event_dictionary.TryGetValue(eventName, out this_event))
        {
            this_event.RemoveListener(listener);
        }
       
    }
    public static void TriggerEvent(string eventName, object param)
    {
        EventOneArg this_event = null;
        if (instance.event_dictionary.TryGetValue(eventName, out this_event))
        {
            this_event.Invoke(param);
        }
    }
}

