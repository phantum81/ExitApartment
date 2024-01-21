using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventListener : MonoBehaviour
{
    public GameEvent eventSo;
    public UnityEvent response;

    private void OnEnable()
    {
        eventSo.RegisterListener(this);
    }
    private void OnDisable()
    {
        eventSo.UnregisterListener(this);
    }

    public void OnEventRaise()
    {
        response.Invoke();
    }

}
