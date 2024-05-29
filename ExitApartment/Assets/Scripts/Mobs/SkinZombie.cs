using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SkinZombie : MonoBehaviour, IEnemyContect
{
    private NavMeshAgent agent;

    [Header("���� �ݰ�"),SerializeField]
    private float patrolRadius;
    
    [Header("���ð�"),SerializeField]
    private float waitTime;
    [Header("ȸ���ӵ�"), SerializeField]
    private float rotSpeed;
    [Header("�ӵ�"), SerializeField]
    private float speed;
    [Header("Ž�� ����"), SerializeField]
    private float height;
    [Header("�̵�����"), SerializeField]
    private float velocityLerp;

    [Header("Ž���ݰ�"), SerializeField]
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
                // ���� �ð� ������ �i�ƿ���
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
                // ��ȿ 2���� patrol
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
                // ���̴� ���
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
