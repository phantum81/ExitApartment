using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Events;

public class GravityCollider : MonoBehaviour, IEventContect
{
    
    public UnityEvent onGravityEvent;
    public UnityEvent onAliveEvent;
    public void OnContect(ESOEventType _type)
    {
        switch (_type)
        {
            case ESOEventType.OnGravity:
                OnGravirty();
                break;
            case ESOEventType.OnClear12F:
                OnAlive();
                break;

        }
        GameManager.Instance.eventMgr.ChangeStageState(1);
        GameManager.Instance.eventMgr.ChangeEventType((int)_type);
    }
    public void OnGravirty()
    {
        onGravityEvent.Invoke();
    }
    public void OnAlive()
    {
        onAliveEvent.Invoke();
    }
}
