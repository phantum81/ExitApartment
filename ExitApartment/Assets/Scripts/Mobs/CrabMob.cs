using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CrabMob : MonoBehaviour, IEnemyContect
{
    
    [Header("대기시간"), SerializeField]
    private float waitTime;

    [Header("탐색반경"), SerializeField]
    private float lookRadius;

    [Header("시야반경"), SerializeField]
    private float viewAngle;


    private float timer;
    private NavMeshAgent agent;
    private EenemyState state = EenemyState.Idle;
    private Animator anim;
    private UnitManager unitMgr;
    private EventManager eventMgr;
    private Transform target;
    private Transform curTarget;
    private bool isEvent=false;

    void Start()
    {
        anim = transform.GetComponent<Animator>();
        eventMgr = GameManager.Instance.eventMgr;
        unitMgr = GameManager.Instance.unitMgr;
        agent = GetComponent<NavMeshAgent>();
        timer = waitTime;
        state = EenemyState.None;

    }
    void Update()
    {
        if (!isEvent) return;

        anim.SetFloat("Speed", agent.speed);
        UpdateTarget();

        if (state != EenemyState.Attack || state != EenemyState.None)
        {
            UpdateState();
        }

        PerformActionBasedOnState();
    }


    void UpdateTarget()
    {
        target = unitMgr.MobCtr.GetLookingTarget(transform, lookRadius, 1 << 7);
        if (target && state == EenemyState.None)
            state = EenemyState.Idle;
    }

    void UpdateState()
    {
        if (unitMgr.MobCtr.CheckTargetInSight(transform, target, viewAngle))
        {
            state = EenemyState.Chase;
        }
        else if (!target && state != EenemyState.None)
        {
            state = EenemyState.Idle;
        }
    }

    void PerformActionBasedOnState()
    {
        switch (state)
        {
            case EenemyState.Idle:
                HandleIdleState();
                break;

            case EenemyState.Chase:
                HandleChaseState();
                break;

            case EenemyState.Attack:
                HandleAttackState();
                break;
        }
    }

    void HandleIdleState()
    {
        agent.speed = 0f;
        anim.SetTrigger("Interact");
    }

    void HandleChaseState()
    {
        if (!target) return;

        agent.SetDestination(target.position);
        agent.speed = 5f;
    }

    void HandleAttackState()
    {
        anim.SetTrigger("Attack");
        state = EenemyState.None;
    }



    public void OnContect()
    {
        if(state != EenemyState.None)
        {
            state = EenemyState.Attack;
            GameManager.Instance.unitMgr.GetContectTarget(this.transform);
            eventMgr.ChangePlayerState(EplayerState.Die);
        }

    }
    public void OnMagicStoneEvent()
    {
        isEvent = true;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
        Vector3 leftBoundary = Quaternion.Euler(0, -viewAngle / 2, 0) * transform.forward * lookRadius;
        Vector3 rightBoundary = Quaternion.Euler(0, viewAngle / 2, 0) * transform.forward * lookRadius;

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + leftBoundary);
        Gizmos.DrawLine(transform.position, transform.position + rightBoundary);


        Gizmos.color = Color.red;
        if(target)
            Gizmos.DrawRay(transform.position, (target.position - transform.position).normalized);
    }

}
