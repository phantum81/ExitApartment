using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFall<T> : IState<T> where T : MonoBehaviour
{
    public void OperateEnter(T _player)
    {
        GameManager.Instance.cameraMgr.ChangeCameraState((int)HFSM<EplayerMoveState, PlayerController>.Instance.CurState);
    }

    public void OperateUpdate(T _player)
    {

    }

    public void OperateExit(T _player)
    {

    }
}
