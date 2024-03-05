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
            _post.GrainOn(true);
            _post.ChromaticAberrationOn(true);
            _post.StartCoroutine(_post.LensDistortion(true));
        }

    }

    public void OperateUdate(T _send)
    {

    }

    public void OperateExit(T _send)
    {

    }
}
