using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerStateDie<T> : IState<T> where T : MonoBehaviour
{

    public void OperateEnter(T _send)
    {
        if (_send is PlayerPostProcess)
        {
            PlayerPostProcess _post = _send as PlayerPostProcess;
            _post.StartCoroutine(_post.GrainOn(true));
            _post.VignetteOn(true);
            _post.StartCoroutine(_post.ChromaticAberrationOn(true));
        }
        else if (_send is OnDeadCameraController)
        {
            OnDeadCameraController _deadCam = _send as OnDeadCameraController;
            _deadCam.StopAllCoroutines();
            _deadCam.StartCoroutine(_deadCam.DeadCam());
        }
        GameManager.Instance.eventMgr.ChangeStageState(1);
    }

    public void OperateUpdate(T _send)
    {


    }

    public void OperateExit(T _send)
    {

    }
}
