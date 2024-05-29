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

    private EplayerState ePlayerCurState;
    public EplayerState EPlayerCurState => ePlayerCurState;

    private float limitTime = 3f;
    private float curTime = 0f;


    private CameraManager cameraMgr;
    private UnitManager unitMgr;

    private GameObject seePoint;

    void Start()
    {
        Init();
    }

    private void Update()
    {
        
        statePostHFSM.Update(playerPost);
        stateDeadCamHFSM.Update(deadCamCtr);
        ePlayerCurState = statePostHFSM.CurState;


        if (curTime > limitTime)
        {
            ChangePlayerState(EplayerState.Die);
        }


        if (cameraMgr.CheckObjectInCamera(seePoint))
        {
            ChangePlayerState(EplayerState.MentalDamage);
            curTime += Time.deltaTime;
        }
        else
            curTime = 0f;





    }



    private void Init()
    {
        

        stateDeadCamHFSM = HFSM<EplayerState, OnDeadCameraController>.Instance;
        statePostHFSM = HFSM<EplayerState, PlayerPostProcess>.Instance;  
        
        statePostHFSM.ChangeState(EplayerState.None, playerPost);
        stateDeadCamHFSM.ChangeState(EplayerState.None, deadCamCtr);

        playerPost = transform.GetComponent<PlayerPostProcess>();
        cameraMgr = GameManager.Instance.cameraMgr;
        unitMgr = GameManager.Instance.unitMgr;
        seePoint = unitMgr.MobDic[EMobType.Pumpkin].GetComponent<Pumpkin>().SeePoint;
    }
    public void ChangePlayerState(EplayerState _state)
    {
        statePostHFSM.ChangeState(_state, playerPost);
        stateDeadCamHFSM.ChangeState(_state, deadCamCtr);
    }


}
