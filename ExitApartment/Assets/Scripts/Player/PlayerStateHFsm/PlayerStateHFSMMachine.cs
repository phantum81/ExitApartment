using System;
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


    private Coroutine curPumpkinTimer = null;
    private Coroutine curOutsideTimer = null;
    private CameraManager cameraMgr;
    private UnitManager unitMgr;
    private GameObject seePoint;




    void Start()
    {
        Init();
        
    }

    private void Update()
    {
        
        ePlayerCurState = statePostHFSM.CurState;


        //if (curTime > limitTime)
        //{
        //    ChangePlayerState(EplayerState.Die);
        //}


        //if (cameraMgr.CheckObjectInCamera(seePoint))
        //{
        //    ChangePlayerState(EplayerState.MentalDamage);
        //    GameManager.Instance.Timer(limitTime, EplayerState.Die, ChangePlayerState);
        //    curTime += Time.deltaTime;
        //}
        //else
        //    curTime = 0f;


        if (cameraMgr.CheckObjectInCamera(seePoint))
        {

            ChangePlayerState(EplayerState.MentalDamage);
            if (curPumpkinTimer == null)
                curPumpkinTimer = StartCoroutine(GameManager.Instance.CoTimer(limitTime, EplayerState.Die, ChangePlayerState));
        }
        else if(curPumpkinTimer != null)
        {
            StopCoroutine(curPumpkinTimer);
            curPumpkinTimer = null;
        }
            

        


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
        cameraMgr = GameManager.Instance.cameraMgr;
        unitMgr = GameManager.Instance.unitMgr;
        seePoint = unitMgr.MobDic[EMobType.Pumpkin].GetComponent<Pumpkin>().SeePoint;

        //Action onResetEvent = () => ChangePlayerState(EplayerState.None);

        //GameManager.Instance.onHomeReset += onResetEvent;
        //GameManager.Instance.onNothingReset += onResetEvent;
        //GameManager.Instance.onFallReset += onResetEvent;
        //GameManager.Instance.onForestReset += onResetEvent;
        //GameManager.Instance.onEscapeReset += onResetEvent;
    }
    public void ChangePlayerState(EplayerState _state)
    {
        statePostHFSM.ChangeState(_state, playerPost);
        stateDeadCamHFSM.ChangeState(_state, deadCamCtr);
    }




    public void OutSideEnter()
    {
        ChangePlayerState(EplayerState.MentalDamage);
        if (curOutsideTimer == null)
            curOutsideTimer = StartCoroutine(GameManager.Instance.CoTimer(limitTime, EplayerState.Die, ChangePlayerState));
    }
    public void OutSideExit()
    {
        StopCoroutine(curOutsideTimer);
        curOutsideTimer = null;
    }

}
