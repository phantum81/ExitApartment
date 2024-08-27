using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class BatMob : Mob
{
    private NavMeshAgent agent;
    [Header("가짜 방 생성지점"), SerializeField]
    private Transform fakeRoomSpawn;
    [Header("가짜 방 사라지는지점"), SerializeField]
    private Transform fakeRoomHide;
    [Header("핑크가든  생성지점"), SerializeField]
    private Transform pinkGardenSpawn;
    [Header("핑크가든탈출  생성지점"), SerializeField]
    private Transform pinkGardenExitSpawn;


    [Header("걷기 속력"), SerializeField]
    private float speed;
    [Header("달리기 속력"), SerializeField]
    private float runSpeed;
    [Header("회전 속력"), SerializeField]
    private float rotSpeed;

    [Header("데드 뷰"), SerializeField]
    private Transform deadView;
    [Header("보이는 포인트"), SerializeField]
    private Transform seePoint;
    public BlinkLight mobLight;

    [Header("사운드"), SerializeField]
    private SoundController soundCtr;
    private Animator anim;

    private CameraManager cameraMgr;
    
    private Transform target;
    
    private EEscapeRoomEvent eEscapeRoomEventState;
    void Start()
    {
        Init();
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        unitMgr.ShowObject(transform, false);
        agent.angularSpeed = rotSpeed;
        target = unitMgr.PlayerCtr.Player;
        cameraMgr = GameManager.Instance.cameraMgr;
        
        unitMgr.SeePointsDic.Add(ESeePoint.Bat, seePoint);
        soundCtr.AudioPath = GameManager.Instance.soundMgr.SoundList[150];
    }


    void Update()
    {
        anim.SetFloat("Speed", agent.speed);


        switch (eEnemyState)
        {
            case EenemyState.None:
                agent.speed = 0;
                break;
            case EenemyState.Idle:
                agent.speed = 0;
                break;
            case EenemyState.Patrol:

                break;
            case EenemyState.Chase:
                ChaseTarget(target, false);
                agent.speed = speed;
                break;
            case EenemyState.Attack:
                anim.SetTrigger("Attack");
                eEnemyState = EenemyState.None;
                break;
            case EenemyState.RunAway:
                agent.speed = speed;
                ChaseTarget(fakeRoomHide, true);

                break;

            default: break;

        }


        switch (eEscapeRoomEventState)
        {
            case EEscapeRoomEvent.None:
                break;
            case EEscapeRoomEvent.FakeRoom:

                break;
            case EEscapeRoomEvent.PinkGarden:
                if (cameraMgr.CheckObjectInCamera(unitMgr.SeePointsDic[ESeePoint.Bat], 100f))
                {
                    eEnemyState = EenemyState.Chase;
                }
                else
                {
                    eEnemyState = EenemyState.Idle;
                }
                break;
            case EEscapeRoomEvent.ExitRoom:
                eEnemyState = EenemyState.Chase;
                break;
        }





    }

    public void StepSound()
    {
        soundCtr.Play();
    }

    public override void OnContect()
    {
        if (eEnemyState != EenemyState.None)
        {
            eEnemyState = EenemyState.Attack;

            GameManager.Instance.unitMgr.SetContectTarget(deadView);
            eventMgr.ChangePlayerState(EplayerState.Die);

        }

    }


    public void ShowBatInFakeRoom()
    {
        eEscapeRoomEventState = EEscapeRoomEvent.FakeRoom;
        ShowBat(fakeRoomSpawn);
    }

    public void ShowBatInPinkGarden()
    {
        eEscapeRoomEventState = EEscapeRoomEvent.PinkGarden;
        ShowBat(pinkGardenSpawn);
    }
    public void ShowBatExitPinkGarden()
    {
        eEscapeRoomEventState = EEscapeRoomEvent.ExitRoom;
        ShowBat(pinkGardenExitSpawn);
    }



    private void ShowBat(Transform _spawnPos)
    {
        
        eEnemyState = EenemyState.Idle;
        

        transform.position = _spawnPos.position;
        transform.rotation = _spawnPos.rotation;
        unitMgr.ShowObject(transform, true);
    }





    public void RunAway()
    {        
        eEnemyState = EenemyState.RunAway;
    }

    public void HideBat()
    {
        unitMgr.ShowObject(transform, false);
    }

    
    protected override void Init()
    {
        base.Init();
    }

    public void ChaseTarget(Transform _target, bool _isSurprise)
    {
        mobLight.transform.position = new Vector3(transform.position.x, mobLight.transform.position.y, transform.position.z);
        agent.SetDestination(_target.position);
        if (agent.remainingDistance <= 0.2f && !agent.pathPending)
        {
            if (_isSurprise)
            {
                mobLight.TurnOff();
                unitMgr.ShowObject(transform, false);

            }
            eEscapeRoomEventState = EEscapeRoomEvent.None;
            eEnemyState = EenemyState.None;
        }

    }



}
