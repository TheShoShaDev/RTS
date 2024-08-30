using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class EventManager : MonoBehaviour
{
    private Dictionary<string, UnityEvent> _events = new Dictionary<string, UnityEvent>();
    private static EventManager _eventManager;

    public void AddListener<T>(string eventName, UnityAction<T> listener)
    {
        UnityEvent evt = null;
        if (_events.TryGetValue(eventName, out evt))
        {
            evt.AddListener((UnityAction)(object)listener);
        }
        else
        {
            evt = new UnityEvent();
            evt.AddListener((UnityAction)(object)listener);
            _events.Add(eventName, evt);
        }
    }

    public void RemoveListener<T>(string eventName, UnityAction<T> listener)
    {
        if (_eventManager == null) return;
        UnityEvent evt = null;
        if (_events.TryGetValue(eventName, out evt))
            evt.RemoveListener((UnityAction)(object)listener);
    }

    public void TriggerEvent(string eventName)
    {
        UnityEvent evt = null;
        if (_events.TryGetValue(eventName, out evt))
            evt.Invoke();
    }
}
