using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GravityCollider : MonoBehaviour, IContect
{

    public UnityEvent onGravityEvent;

    public void OnContect()
    {
        OnGravirty();
        GameManager.Instance.eventMgr.ChangeStageState(1);
        GameManager.Instance.eventMgr.ChangeEventType(0);
    }
    public void OnGravirty()
    {
        onGravityEvent?.Invoke();
    }
}
