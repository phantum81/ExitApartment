using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class BatMob : Mob
{
    private NavMeshAgent agent;
    [Header("��¥ �� ��������"), SerializeField]
    private Transform fakeRoomSpawn;
    [Header("��¥ �� �����������"), SerializeField]
    private Transform fakeRoomHide;
    [Header("��� �� ��������"), SerializeField]
    private Transform exitRoomSpawn;

    [Header("�ȱ� �ӷ�"), SerializeField]
    private float walkSpeed;
    [Header("�޸��� �ӷ�"), SerializeField]
    private float runSpeed;
    [Header("ȸ�� �ӷ�"), SerializeField]
    private float rotSpeed;

    [Header("���� ��"), SerializeField]
    private Transform deadView;
    
    public BlinkLight mobLight;
    private Animator anim;

    private Transform runAwayTarget;
    private Transform target;
    void Start()
    {
        Init();
        anim = GetComponent<Animator>();
        agent= GetComponent<NavMeshAgent>();
        unitMgr.ShowObject(transform, false);
        agent.angularSpeed = rotSpeed;

    }

    
    void Update()
    {
        switch(eEnemyState)
        {
            case EenemyState.None: 
                break;
            case EenemyState.Idle:
                anim.CrossFade("Idle",0f);
                break;
            case EenemyState.Patrol:
                anim.CrossFade("Walk", 0.2f);
                break;
            case EenemyState.Chase:
                anim.CrossFade("Chase", 0.2f);
                Chase();
                break;
            case EenemyState.Attack:
                anim.CrossFade("Attack", 0.2f);
                break;
            case EenemyState.RunAway:
                anim.CrossFade("Chase", 0.2f);
                RunAway();
                break;

            default: break;

        }
    }



    public override void OnContect()
    {
        if (eEnemyState != EenemyState.None)
        {
            eEnemyState = EenemyState.Attack;

            GameManager.Instance.unitMgr.SetContectTarget(deadView);
            eventMgr.ChangePlayerState(EplayerState.Die);
            eEnemyState = EenemyState.None;
        }

    }


    public void ShowBatInFakeRoom()
    {
        unitMgr.ShowObject(transform, true);

        transform.SetPositionAndRotation(fakeRoomSpawn.position, fakeRoomSpawn.rotation);
    }

    public void RunAway()
    {
        StartCoroutine(ChaseTarget(fakeRoomHide, true));
    }

    public void Chase()
    {
        StartCoroutine(ChaseTarget(target, true));
    }

    
    protected override void Init()
    {
        base.Init();
    }

    IEnumerator ChaseTarget(Transform _target , bool _isSurprise)
    {
        mobLight.transform.position = new Vector3(transform.position.x, mobLight.transform.position.y, transform.position.z);
        agent.SetDestination(_target.position);
        agent.speed = walkSpeed;
        yield return new WaitUntil(() => agent.remainingDistance <= 0.2f && !agent.pathPending);
        if (_isSurprise)
        {
            mobLight.TurnOff();            
            unitMgr.ShowObject(transform, false);
        }


    }
    
}
