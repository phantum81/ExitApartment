using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStand<T> :  IState<T> where T : MonoBehaviour
{
    public void OperateEnter(T _player)
    {
        GameManager.Instance.cameraMgr.ChangeCameraState((int)HFSM<EplayerMoveState, PlayerController>.Instance.CurState);
        if(_player is PlayerController playerCtr)
        {
            //playerCtr.Rigd.velocity = new Vector3(0f, playerCtr.Rigd.velocity.y, 0f);
        }
    }

    public void OperateUpdate(T _player)
    {
        PlayerController _playerCtr = _player as PlayerController;
        if (_playerCtr != null)
        {
            _playerCtr.Rotate();
            _playerCtr.Rigd.velocity = new Vector3(0f, _playerCtr.Rigd.velocity.y, 0f);
        }
    }

    public void OperateExit(T _player)
    {

    }
}
