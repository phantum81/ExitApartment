using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SkinZombie : MonoBehaviour, IEnemyContect
{
    private NavMeshAgent agent;

    [Header("순찰 반경"),SerializeField]
    private float patrolRadius;
    
    [Header("대기시간"),SerializeField]
    private float waitTime;


    [Header("탐색반경"), SerializeField]
    private float hearRadius;

    private float timer;

    private EenemyState state = EenemyState.Idle;
    private Animator anim;
    private UnitManager unitMgr;
    private EventManager eventMgr;
    private Transform target;

    void Start()
    {
        anim = transform.GetComponent<Animator>();
        eventMgr = GameManager.Instance.eventMgr;
        unitMgr = GameManager.Instance.unitMgr;
        agent = GetComponent<NavMeshAgent>();
        timer = waitTime;
        state = EenemyState.Idle;
        
    }

    
    void Update()
    {
        anim.SetFloat("Speed", agent.speed);

        target = unitMgr.MobCtr.GetSoundtarget(transform, hearRadius, 1 << 7);
        

        if (EenemyState.Idle != state && EenemyState.Attack != state)
        {
            state = target ? EenemyState.Chase : EenemyState.Patrol;

        }



        switch (state)
        {
            case EenemyState.None:
                break;

            case EenemyState.Idle:
                state = EenemyState.Patrol;
                break;

            case EenemyState.Patrol:
                HandlePatrolState();
                break;

            case EenemyState.Chase:
                HandleChaseState();
                break;

            case EenemyState.Attack:
                anim.SetTrigger("Attack");
                state = EenemyState.None;
                break;
        }




    }
    public void OnContect()
    {
        if(state == EenemyState.None) return;
        state = EenemyState.Attack;
        GameManager.Instance.unitMgr.GetContectTarget(this.transform);
        eventMgr.ChangePlayerState(EplayerState.Die);

    }

    void MoveToRandomPosition()
    {
        Vector3 randomDirection = Random.insideUnitSphere * patrolRadius;
        randomDirection += transform.position;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randomDirection, out navHit, patrolRadius, -1);

        agent.SetDestination(navHit.position);
    }


    private void HandlePatrolState()
    {
        if (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
        {
            agent.speed = 0f;
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                agent.speed = 2f;
                MoveToRandomPosition();
                timer = waitTime;
            }
        }
    }

    private void HandleChaseState()
    {

        if (target || agent.destination == target.position)
        {
            agent.SetDestination(target.position);
            agent.speed = 4f;
        }
    }




    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, hearRadius);

    }




}
