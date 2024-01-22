using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityCollider : MonoBehaviour, IContect
{
    


    public void OnContect()
    {
        GameManager.Instance.eventMgr.ChangeStageState(1);
    }
}
