using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FakeCurboardTriggerCollider : MonoBehaviour, IContect
{
    public UnityEvent OnShowMob;

    public void OnContect()
    {
        OnShowMob.Invoke();
        GameManager.Instance.SetIsCheckCurboard(true);
        Debug.Log("OnContect called");
    }

    public void OnStay()
    {


    }

    public void OnExit()
    {
        
    }
}
