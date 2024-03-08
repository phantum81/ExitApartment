using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMentalDamage<T> : IState<T> where T : MonoBehaviour
{
    public void OperateEnter(T _send)
    {
        if (_send is PlayerPostProcess)
        {
            PlayerPostProcess _post = _send as PlayerPostProcess;
            _post.MentalDamagePostProccess();
        }

    }

    public void OperateUpdate(T _send)
    {

    }

    public void OperateExit(T _send)
    {

    }
}
