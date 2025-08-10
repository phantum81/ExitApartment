using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GravityDieCollider : MonoBehaviour, ISOEventContect
{

    [Header("죽음 이벤트"), SerializeField]
    private UnityEvent onDead12F;

    [Header("생존 이벤트"), SerializeField]
    private UnityEvent onClear12F;

    public void OnContect(ESOEventType _type)
    {

        switch (_type)
        {
            case ESOEventType.OnDie12F:
                OnDead12F();
                GameManager.Instance.eventMgr.ChangeEventType((int)_type);
                GameManager.Instance.eventMgr.ChangeStageState(1);
                transform.GetComponent<Collider>().enabled = false;
                break;
            case ESOEventType.OnClear12F:
                OnClear12F();
                GameManager.Instance.eventMgr.ChangeEventType((int)_type);
                GameManager.Instance.eventMgr.ChangeStageState(0);

                break;


        }
        

    }

    public void OnDead12F()
    {
        onDead12F.Invoke();
    }
    public void OnClear12F()
    {
        onClear12F.Invoke();
    }
}
