using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateHFSMMachine : MonoBehaviour
{
    private HFSM<EplayerState, PlayerPostProcess> statePostHFSM;
    private HFSM<EplayerState, OnDeadCameraController> stateDeadCamHFSM;

    
    private PlayerPostProcess playerPost;
    [SerializeField]
    private OnDeadCameraController deadCamCtr;

    void Start()
    {
        Init();
    }

    private void Update()
    {
        statePostHFSM.Update(playerPost);
        stateDeadCamHFSM.Update(deadCamCtr);
    }



    private void Init()
    {
        

        stateDeadCamHFSM = HFSM<EplayerState, OnDeadCameraController>.Instance;
        statePostHFSM = HFSM<EplayerState, PlayerPostProcess>.Instance;  
        
        statePostHFSM.ChangeState(EplayerState.None, playerPost);
        stateDeadCamHFSM.ChangeState(EplayerState.None, deadCamCtr);

        playerPost = transform.GetComponent<PlayerPostProcess>();


    }
    public void ChangePlayerState(EplayerState _state)
    {
        statePostHFSM.ChangeState(_state, playerPost);
        stateDeadCamHFSM.ChangeState(_state, deadCamCtr);
    }
}
