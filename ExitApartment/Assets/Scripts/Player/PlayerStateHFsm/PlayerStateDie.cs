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
            _post.OnDamageVignette();
            _post.OnMentalDamage();
        }
        else if (_send is OnDeadCameraController)
        {
            OnDeadCameraController _deadCam = _send as OnDeadCameraController;
            _deadCam.StartCoroutine(_deadCam.DeadCam());
        }

    }

    public void OperateUdate(T _send)
    {


    }

    public void OperateExit(T _send)
    {

    }
}
