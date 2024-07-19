using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalk<T> : IState<T> where T : MonoBehaviour
{

    public void OperateEnter(T _player)
    {
        if (_player is PlayerController playerCtr)
            GameManager.Instance.cameraMgr.ChangeCameraState((int)HFSM<EplayerMoveState,PlayerController>.Instance.CurState);

            
    }

    public void OperateUpdate(T _player)
    {
        if (_player is PlayerController playerCtr)
        {
            Transform ground;
            Terrain terrain;
            bool isLongGrass;
            string soundPath;

            ground = playerCtr.GrCheck.GetGroundInfo();
            terrain = ground?.GetComponent<Terrain>();

            if (terrain != null)
            {
                isLongGrass = GameManager.Instance.unitMgr.CheckTerrainDetail(
                    playerCtr.Player.position,
                    terrain.terrainData,
                    terrain,
                    2,
                    0,
                    7
                );

                soundPath = isLongGrass
                    ? GameManager.Instance.soundMgr.SoundList[6]
                    : GameManager.Instance.soundMgr.SoundList[4];
            }
            else
            {
                soundPath = GameManager.Instance.soundMgr.SoundList[0];
            }

            playerCtr.SoundCtr.UpdateSound(soundPath);

            playerCtr.Move(playerCtr.InputDir, playerCtr.WalkSpeed);
            playerCtr.Rotate();

        }
    }

    public void OperateExit(T _player)
    {
        if (_player is PlayerController playerCtr)
            playerCtr.SoundCtr.Stop();
    }
}
