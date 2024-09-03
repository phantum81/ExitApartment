using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TelePortCollider : MonoBehaviour, IContect
{
    public UnityEvent onTelePort;



    public void OnContect()
    {
        onTelePort.Invoke();
        GameManager.Instance.SetEscapeClearFloor(true);
        GameManager.Instance.ChangeFloorLevel(EFloorType.Escape888B);
        
    }

    public void OnStay()
    {


    }

    public void OnExit()
    {
       
    }

}
