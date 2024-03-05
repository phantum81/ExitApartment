using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateNone<T> : IState<T> where T : MonoBehaviour
{
    public void OperateEnter(T _send)
    {

        //if (_send is PlayerPostProcess)
        //{
        //    PlayerPostProcess _post = _send as PlayerPostProcess;
        //    _post.InitPostProcess();

        //}
    }

    public void OperateUdate(T _send)
    {

    }

    public void OperateExit(T _send)
    {

    }
}
