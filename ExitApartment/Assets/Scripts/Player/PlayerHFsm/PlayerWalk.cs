using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalk<T> : IState<T> where T : MonoBehaviour
{
    
    public void OperateEnter(T _player)
    {
        GameManager.Instance.cameraMgr.ChangeCameraState((int)HFSM<EplayerMoveState,PlayerController>.Instance.CurState);
        if (_player is PlayerController playerCtr)
            playerCtr.SoundCtr.AudioPath = GameManager.Instance.soundMgr.SoundList[0];
    }

    public void OperateUpdate(T _player)
    {
        if (_player is PlayerController playerCtr)
        {
            playerCtr.Move(playerCtr.InputDir, playerCtr.WalkSpeed);
            playerCtr.Rotate();
            if (!playerCtr.SoundCtr.CheckPlaying())
                playerCtr.SoundCtr.Play();
        }
    }

    public void OperateExit(T _player)
    {
        if (_player is PlayerController playerCtr)
            playerCtr.SoundCtr.Stop();
    }
}
