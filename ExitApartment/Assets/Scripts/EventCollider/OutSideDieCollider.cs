using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;

public class OutSideDieCollider : MonoBehaviour, IContect
{
    public UnityEvent onOutSideDamge;
    public UnityEvent onReturn;

    public void OnContect()
    {
        onOutSideDamge.Invoke();
        //3√ »ƒªÁ∏¡
    }

    public void OnStay()
    {
        

    }

    public void OnExit()
    {
        onReturn.Invoke();
    }
}
