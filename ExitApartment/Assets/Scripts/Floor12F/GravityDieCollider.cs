using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityDieCollider : MonoBehaviour, IContect
{
    public void OnContect()
    {
        GameManager.Instance.eventMgr.ChangeStageState(2);
    }
}
