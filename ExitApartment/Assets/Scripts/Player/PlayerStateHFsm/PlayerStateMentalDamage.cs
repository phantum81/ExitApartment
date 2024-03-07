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
            _post.StopAllCoroutinesInList();
            _post.CurCoroutine.Add(_post.StartCoroutine(_post.PostProccessEffectOn(EpostProcessType.Grain)));
            _post.CurCoroutine.Add(_post.StartCoroutine(_post.PostProccessEffectOn(EpostProcessType.LensDistortion, -40f)));
            _post.CurCoroutine.Add(_post.StartCoroutine(_post.PostProccessEffectOn(EpostProcessType.ChromaticAberration)));
        }

    }

    public void OperateUpdate(T _send)
    {

    }

    public void OperateExit(T _send)
    {

    }
}
