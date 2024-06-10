using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRun<T> : IState<T> where T : MonoBehaviour
{
    
    public void OperateEnter(T _player)
    {
        GameManager.Instance.cameraMgr.ChangeCameraState((int)HFSM<EplayerMoveState, PlayerController>.Instance.CurState);
        if (_player is PlayerController playerCtr)
            playerCtr.SoundCtr.AudioPath = GameManager.Instance.soundMgr.SoundList[1];
    }

    public void OperateUpdate(T _player)
    {
        if (_player is PlayerController playerCtr)
        {
            playerCtr.Move(playerCtr.InputDir, playerCtr.RunSpeed);
            playerCtr.Rotate();
            if (!playerCtr.SoundCtr.CheckPlaying())
                playerCtr.SoundCtr.Play();
        }

    }

    public void OperateExit(T _player)
    {
        if (_player is PlayerController playerCtr)
        {
            playerCtr.SoundCtr.Stop();

        }
        GameManager.Instance.cameraMgr.CameraCtr.ResetCamYPosition();

    }
}
