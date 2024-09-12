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
    public UnityEvent stepSoundOn;
    private bool wasIn = false;
    private bool wasOut = false;




    public void OnContect()
    {
        if (wasIn) return;
        showBat?.Invoke();
        soundOn?.Invoke();
        stepSoundOn?.Invoke();
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
