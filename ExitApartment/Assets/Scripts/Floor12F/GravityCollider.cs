using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Events;

public class GravityCollider : MonoBehaviour, IEventContect
{
    
    public UnityEvent onGravityEvent;
    public void OnContect(ESOEventType _type)
    {
        switch (_type)
        {
            case ESOEventType.OnGravity:
                OnGravirty();
                GameManager.Instance.eventMgr.ChangeEventType((int)_type);
                break;
            default:                
                break;
        }
        GameManager.Instance.eventMgr.ChangeStageState(1);
    }



    public void OnGravirty()
    {
        onGravityEvent.Invoke();
    }


    private void OnTriggerEnter(Collider other)
    {
        IGravityChange _col = other.GetComponent<IGravityChange>();
        _col?.OnGravityChange();
    }
}
