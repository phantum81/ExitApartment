using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FakeCurboardHideTriggerCollider: MonoBehaviour, IContect
{
    public UnityEvent onSetFalse;
    private bool isDoit = false;
    public void OnContect()
    {
        if (GameManager.Instance.unitMgr.MobDic[EMobType.Bat].gameObject.activeSelf && !isDoit)
        {
            isDoit = true;
            onSetFalse.Invoke();
        }
        
    }

    public void OnStay()
    {


    }

    public void OnExit()
    {
        
    }
}
