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
    [Header("생존 이벤트"), SerializeField]
    private UnityEvent onClear12F;
    public void OnContect(ESOEventType _type)
    {
        switch (_type)
        {
            case ESOEventType.OnGravity:
                OnGravirty();
                GameManager.Instance.eventMgr.ChangeEventType((int)_type);
                GameManager.Instance.eventMgr.ChangeStageState(1);
                break;
            case ESOEventType.OnClear12F:
                OnClear12F();
                GameManager.Instance.eventMgr.ChangeEventType((int)_type);
                GameManager.Instance.eventMgr.ChangeStageState(0);
                transform.GetComponent<Collider>().enabled = false;
                break;
            default:                
                break;
        }
        
    }



    public void OnGravirty()
    {
        onGravityEvent.Invoke();
    }
    public void OnClear12F()
    {
        onClear12F.Invoke();
    }

    private void OnTriggerEnter(Collider other)
    {
        IGravityChange _col = other.GetComponent<IGravityChange>();
        _col?.OnGravityChange();
    }
}
