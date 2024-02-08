using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class EventListener : MonoBehaviour
{
    public GameEvent eventSo;
    public UnityEvent response;
    public ESOEventType eventType;
    private void OnEnable()
    {
        switch (eventType)
        {
            case ESOEventType.OnGravity:
                eventSo.GravityRegisterListener(this);
                break;
            case ESOEventType.OnDie12F:
                eventSo.Die12FRegisterListener(this); 
                break;
        }

        
    }
    private void OnDisable()
    {
        switch (eventType)
        {
            case ESOEventType.OnGravity:
                eventSo.GravityUnregisterListener(this);
                break;
            case ESOEventType.OnDie12F:
                eventSo.Die12FUnregisterListener(this);
                break;
        }
    }

    public void OnEventRaise()
    {
        response.Invoke();
    }

}
