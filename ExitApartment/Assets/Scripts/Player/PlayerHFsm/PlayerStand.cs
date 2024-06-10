using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStand<T> :  IState<T> where T : MonoBehaviour
{
    public void OperateEnter(T _player)
    {
        GameManager.Instance.cameraMgr.ChangeCameraState((int)HFSM<EplayerMoveState, PlayerController>.Instance.CurState);
        
    }

    public void OperateUpdate(T _player)
    {
        PlayerController _playerCtr = _player as PlayerController;
        if (_playerCtr != null)
        {
            _playerCtr.Rotate();
        }
    }

    public void OperateExit(T _player)
    {

    }
}
