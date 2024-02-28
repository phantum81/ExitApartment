using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GravityDieCollider : MonoBehaviour, IEventContect
{

    [Header("Ʈ���� �̺�Ʈ"), SerializeField]
    private UnityEvent onDead12F;

    public void OnContect(ESOEventType _type)
    {
        OnDead12F();
        GameManager.Instance.eventMgr.ChangeStageState(1);
        GameManager.Instance.eventMgr.ChangeEventType(1);

    }

    public void OnDead12F()
    {
        onDead12F.Invoke();
    }

}
