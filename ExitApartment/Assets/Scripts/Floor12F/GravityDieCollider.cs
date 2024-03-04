using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GravityDieCollider : MonoBehaviour, IEventContect
{

    [Header("���� �̺�Ʈ"), SerializeField]
    private UnityEvent onDead12F;
    [Header("���� �̺�Ʈ"), SerializeField]
    private UnityEvent onClear12F;


    public void OnContect(ESOEventType _type)
    {
        switch (_type)
        {
            case ESOEventType.OnDie12F:
                OnDead12F();
                GameManager.Instance.eventMgr.ChangeEventType((int)_type);
                break;
            case ESOEventType.OnClear12F:
                OnClear12F();
                GameManager.Instance.eventMgr.ChangeEventType((int)_type);
                break;

        }
        GameManager.Instance.eventMgr.ChangeStageState(1);

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
