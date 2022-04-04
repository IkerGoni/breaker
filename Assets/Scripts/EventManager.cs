using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


/// <summary>
/// Not my script. Google search
/// </summary>
public class EventManager : MonoBehaviour {
    private Dictionary<string, Action<Dictionary<string, object>>> eventDictionary;

    private static EventManager eventManager;

    public static EventManager Instance;
    
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }

        Instance = this;
        Instance.Init();
    }

    void Init() {
        if (eventDictionary == null) {
            eventDictionary = new Dictionary<string, Action<Dictionary<string, object>>>();
        }
    }

    public static void StartListening(string eventName, Action<Dictionary<string, object>> listener) {
        Action<Dictionary<string, object>> thisEvent;
        if (Instance.eventDictionary.TryGetValue(eventName, out thisEvent)) {
            thisEvent += listener;
            Instance.eventDictionary[eventName] = thisEvent;
        } else {
            thisEvent += listener;
            Instance.eventDictionary.Add(eventName, thisEvent);
        }
    }

    public static void StopListening(string eventName, Action<Dictionary<string, object>> listener) {
        if (eventManager == null) return;
        Action<Dictionary<string, object>> thisEvent;
        if (Instance.eventDictionary.TryGetValue(eventName, out thisEvent)) {
            thisEvent -= listener;
            Instance.eventDictionary[eventName] = thisEvent;
        }
    }

    public static void TriggerEvent(string eventName, Dictionary<string, object> message = null) {
        Action<Dictionary<string, object>> thisEvent = null;
      
        if (Instance.eventDictionary.TryGetValue(eventName, out thisEvent)) 
        {
            thisEvent.Invoke(message);
        }
    }
}