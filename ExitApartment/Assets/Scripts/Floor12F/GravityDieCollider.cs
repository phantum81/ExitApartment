using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GravityDieCollider : MonoBehaviour, IContect
{
    [SerializeField]
    private float shake = 1f;
    [SerializeField]
    private float amount = 1f;
    [Header("트리거 이벤트"), SerializeField]
    private UnityEvent onDead12F;
    public void OnContect()
    {
        OnDead12F();
        GameManager.Instance.eventMgr.ChangeStageState(1);
        GameManager.Instance.eventMgr.ChangeEventType(1);

    }

    public void OnDead12F()
    {
        onDead12F?.Invoke();
    }

}
