using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHFSMMachine : MonoBehaviour
{
    private HFSM<EplayerMoveState, PlayerController> MoveHFSM;    
    

    private InputManager inputMgr;
    private EventManager eventMgr;
    private PlayerController playerCtr;
    
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        if(eventMgr.eStageState == EstageEventState.None)
        {
            if (inputMgr.InputDir != Vector3.zero)
            {
                MoveHFSM.ChangeState(EplayerMoveState.Walk, playerCtr);

                if (inputMgr.IsShift)
                    MoveHFSM.ChangeState(EplayerMoveState.Run, playerCtr);
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
        MoveHFSM = HFSM<EplayerMoveState, PlayerController>.Instance;
        
        

        eventMgr = GameManager.Instance.eventMgr;
        MoveHFSM.ChangeState(EplayerMoveState.None, playerCtr);
        
    }
}
