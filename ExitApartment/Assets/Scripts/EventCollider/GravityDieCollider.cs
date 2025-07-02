using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GravityDieCollider : MonoBehaviour, ISOEventContect
{

    [Header("Á×À½ ÀÌº¥Æ®"), SerializeField]
    private UnityEvent onDead12F;



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


        }
        

    }

    public void OnDead12F()
    {
        onDead12F.Invoke();
    }

}
