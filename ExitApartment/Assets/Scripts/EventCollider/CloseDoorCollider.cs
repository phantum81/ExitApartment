using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CloseDoorCollider : MonoBehaviour, ISOEventContect
{
    public UnityEvent onClose;
    [SerializeField]
    private ElevatorController elevatorCtr;
    private bool bite = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    public void OnContect(ESOEventType _type)
    {
        bite = true;
        elevatorCtr.StopCoroutine(elevatorCtr.CurCoroutine);
        elevatorCtr.eleWork = EElevatorWork.Closing;
        onClose.Invoke();
        transform.gameObject.SetActive(false);
        GameManager.Instance.achievementCtr.Unlock(ConstBundle.ROOM436_BITE);
       
    }

    
    private void OnDisable()
    {
        if (!bite && elevatorCtr.eCurFloor != EFloorType.Nothing436A && GameManager.Instance.eFloorType == EFloorType.Nothing436A)
        {
            GameManager.Instance.achievementCtr.Unlock(ConstBundle.ROOM436_PASS);
        }
    }
}
