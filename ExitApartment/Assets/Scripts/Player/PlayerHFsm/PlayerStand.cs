using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStand<T> :  IState<T> where T : MonoBehaviour
{
    public void OperateEnter(T _player)
    {
        PlayerController _playerCtr = _player as PlayerController;
        if (_playerCtr != null)
        {
            
        }
    }

    public void OperateUdate(T _player)
    {

    }

    public void OperateExit(T _player)
    {

    }
}
