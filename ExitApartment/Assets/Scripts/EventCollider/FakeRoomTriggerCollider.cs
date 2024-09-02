using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FakeRoomTriggerCollider : MonoBehaviour, IContect
{
    public UnityEvent showBat;
    public UnityEvent hideBat;
    public UnityEvent soundOn;
    
    private bool wasIn = false;
    private bool wasOut = false;




    public void OnContect()
    {
        if (wasIn) return;
        showBat?.Invoke();
        soundOn?.Invoke();
        wasIn = true;
    }
    public void OnExit()
    {
        if (wasOut) return;
        hideBat?.Invoke();
        wasOut = true;
    }
    public void OnStay()
    {

    }
}
