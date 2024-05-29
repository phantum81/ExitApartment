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
    [Header("회전속도"), SerializeField]
    private float rotSpeed;
    [Header("속도"), SerializeField]
    private float speed;
    [Header("탐색 높이"), SerializeField]
    private float height;
    [Header("이동러프"), SerializeField]
    private float velocityLerp;

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
        eventMgr = GameManager.Instance.eventMgr;
        unitMgr = GameManager.Instance.unitMgr;
        agent = GetComponent<NavMeshAgent>();
        timer = waitTime;
        MoveToRandomPosition();
    }

    
    void Update()
    {

        if(EenemyState.Idle != state && EenemyState.Attack != state)
        {
            if (unitMgr.MobCtr.CheckTargetAudio(transform, hearRadius, 1 << 7))
            {
                // 일정 시간 동안은 쫒아오게
                state = EenemyState.Chase;
            }
            else
                state = EenemyState.Patrol;
        }



        switch (state)
        {
            case EenemyState.None:

                break;
            case EenemyState.Idle:
                // 포효 2초후 patrol
                state = EenemyState.Patrol;
                break;
            case EenemyState.Patrol:

                if (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
                {
                    timer -= Time.deltaTime;

                    if (timer <= 0f)
                    {
                        MoveToRandomPosition();
                        timer = waitTime;
                    }
                }
                break;
            case EenemyState.Chase:
                unitMgr.MobCtr.ChaseTarget(transform, target, speed, rotSpeed, height, velocityLerp);                
                break;

            case EenemyState.Attack:
                // 죽이는 모션
                break;
        }




    }
    public void OnContect()
    {
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

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, hearRadius);
    }

}
