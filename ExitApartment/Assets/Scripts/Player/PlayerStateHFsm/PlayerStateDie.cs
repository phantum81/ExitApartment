using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateDie<T> : IState<T> where T : MonoBehaviour
{
    public void OperateEnter(T _player)
    {

        GameManager.Instance.cameraMgr.ChangeCameraState((int)HFSM<EplayerMoveState, PlayerController>.Instance.CurState);
    }

    public void OperateUdate(T _player)
    {

    }

    public void OperateExit(T _player)
    {

    }
}
