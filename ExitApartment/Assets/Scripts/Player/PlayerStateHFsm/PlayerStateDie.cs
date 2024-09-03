
using UnityEngine;

public class PlayerStateDie<T> : IState<T> where T : MonoBehaviour
{
    PlayerController playerCtr;
    public void OperateEnter(T _send)
    {
        
        if (_send is PlayerPostProcess)
        {
            PlayerPostProcess _post = _send as PlayerPostProcess;
            playerCtr = _post.gameObject.GetComponent<PlayerController>();
            _post.StartCoroutine(_post.GrainOn(true));
            _post.StartCoroutine(_post.VignetteOn());
            _post.StartCoroutine(_post.ChromaticAberrationOn(true));
            playerCtr.Rigd.useGravity = false;
            playerCtr.Player.GetComponent<Collider>().enabled = false;
            playerCtr.PSound.SoundCtr.Stop();

        }
        else if (_send is OnDeadCameraController)
        {
            OnDeadCameraController _deadCam = _send as OnDeadCameraController;
            _deadCam.StopAllCoroutines();
            _deadCam.ChangeTimeLineAsset();
            _deadCam.StartCoroutine(_deadCam.DeadCam());
        }
        GameManager.Instance.eventMgr.ChangeStageState(1);
    }

    public void OperateUpdate(T _send)
    {


    }

    public void OperateExit(T _send)
    {
        if (_send is PlayerPostProcess)
        {
            //GameManager.Instance.eventMgr.ChangeStageState(0);
            //GameManager.Instance.RequestReset();
        }
            
    }
}
