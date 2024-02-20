using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateNone<T> : IState<T> where T : MonoBehaviour
{
    public void OperateEnter(T _player)
    {

        
    }

    public void OperateUdate(T _player)
    {

    }

    public void OperateExit(T _player)
    {

    }
}
