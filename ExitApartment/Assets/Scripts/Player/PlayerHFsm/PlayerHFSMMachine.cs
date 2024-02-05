using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHFSMMachine : MonoBehaviour
{
    private HFSM<EplayerMoveState, PlayerController> MoveHFSM;    
    private HFSM<EstageEventState, EventManager> eventHFSM;

    private InputManager inputMgr;    
    private PlayerController playerCtr;
    
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        if(eventHFSM.CurState == EstageEventState.None)
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


        MoveHFSM.Update(playerCtr);
    }

    private void Init()
    {
        
        inputMgr = GameManager.Instance.inputMgr;
        playerCtr = GameManager.Instance.unitMgr.PlayerCtr;
        MoveHFSM = HFSM<EplayerMoveState, PlayerController>.Instance;
        
        eventHFSM = HFSM<EstageEventState, EventManager>.Instance;


        MoveHFSM.ChangeState(EplayerMoveState.None, playerCtr);
        
    }
}
