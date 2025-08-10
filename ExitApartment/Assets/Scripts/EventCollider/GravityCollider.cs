using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Events;

public class GravityCollider : MonoBehaviour, ISOEventContect
{
    [Header("중력 이벤트"), SerializeField]
    private UnityEvent onGravityEvent;

    public void OnContect(ESOEventType _type)
    {
        switch (_type)
        {
            case ESOEventType.OnGravity:
                OnGravirty();
                GameManager.Instance.eventMgr.ChangeEventType((int)_type);
                GameManager.Instance.eventMgr.ChangeStageState(1);
                break;

            default:                
                break;
        }
        
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
