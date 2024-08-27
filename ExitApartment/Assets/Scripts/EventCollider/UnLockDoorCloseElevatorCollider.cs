using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UnLockDoorCloseElevatorCollider : MonoBehaviour, IContect
{
    public UnityEvent onUnlockDoor;
    public UnityEvent onCloseElevator;
    private bool isDoit = false;
    public void OnContect()
    {
        if(GameManager.Instance.unitMgr.MobDic[EMobType.Bat].gameObject.activeSelf && !isDoit)
        {
            onUnlockDoor.Invoke();
            onCloseElevator.Invoke();
            isDoit= true;
        }

    }

    public void OnStay()
    {


    }

    public void OnExit()
    {

    }
}
