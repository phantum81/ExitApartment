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

    [Header("걷는 속도"), SerializeField]
    private float walkSpeed = 2f;
    [Header("달리는 속도"), SerializeField]
    private float runSpeed = 4f;

    [Header("그라운드 체커"), SerializeField]
    private GroundCheck groundCheck;


    [Header("탐색반경"), SerializeField]
    private float hearRadius;

    private float timer;

    private EenemyState state = EenemyState.Idle;
    private Animator anim;
    private UnitManager unitMgr;
    private EventManager eventMgr;
    private Transform target;
    private SoundController soundCtr;
    [SerializeField]
    private SoundController chaseSoundCtr;
    [Header("데드뷰"), SerializeField]
    private Transform deadView;
    void Start()
    {
        anim = transform.GetComponent<Animator>();
        eventMgr = GameManager.Instance.eventMgr;
        unitMgr = GameManager.Instance.unitMgr;
        agent = GetComponent<NavMeshAgent>();
        timer = waitTime;
        state = EenemyState.Idle;

        soundCtr = GetComponent<SoundController>();
        soundCtr.AudioPath = GameManager.Instance.soundMgr.SoundList[71];
        chaseSoundCtr.AudioPath = GameManager.Instance.soundMgr.SoundList[72];
        soundCtr.Play();
        //soundCtr.StartCoroutine(soundCtr.PlayingRandomTimeSound(8f, 14f));
    }

    
    void Update()
    {
        anim.SetFloat("Speed", agent.speed);
        if (EenemyState.None == state)
            return;

        target = unitMgr.MobCtr.GetOverlaptarget(transform, hearRadius, 1 << 7);
        if(target != null)
        {
            if (!target.parent.gameObject.GetComponent<AudioSource>().isPlaying)
            {
                target = null;
            }
                            
        }


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
                if(!chaseSoundCtr.IsPlaying)
                    chaseSoundCtr.Play();
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
        GameManager.Instance.unitMgr.SetContectTarget(deadView);
        soundCtr.AudioPath = GameManager.Instance.soundMgr.SoundList[73];
        soundCtr.Play();
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
                agent.speed = walkSpeed;
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
            agent.speed = runSpeed;
        }
    }





    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, hearRadius);

    }




}
