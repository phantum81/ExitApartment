using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHFSMMachine : MonoBehaviour
{
    private HFSM<EplayerMoveState, PlayerController> MoveHFSM;    
    

    private InputManager inputMgr;
    private EventManager eventMgr;
    private CameraManager cameraMgr;
    private PlayerController playerCtr;
    
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {

        if(cameraMgr.CurCamera == cameraMgr.CameraDic[0] && eventMgr.eStageState == EstageEventState.None)
        {
            if (inputMgr.InputDir != Vector3.zero)
            {
                if (inputMgr.InputDic[EuserAction.Run])
                    MoveHFSM.ChangeState(EplayerMoveState.Run, playerCtr);
                else
                    MoveHFSM.ChangeState(EplayerMoveState.Walk, playerCtr);


            }
            else
                MoveHFSM.ChangeState(EplayerMoveState.Stand, playerCtr);

            
        }
        else
        {
             MoveHFSM.ChangeState(EplayerMoveState.None, playerCtr);
        }

        MoveHFSM.Update(playerCtr);

    }




    private void Init()
    {
        
        inputMgr = GameManager.Instance.inputMgr;
        playerCtr = GameManager.Instance.unitMgr.PlayerCtr;
        cameraMgr = GameManager.Instance.cameraMgr;
        eventMgr = GameManager.Instance.eventMgr;
        MoveHFSM = HFSM<EplayerMoveState, PlayerController>.Instance;


        MoveHFSM.ChangeState(EplayerMoveState.None, playerCtr);
        
    }
}
