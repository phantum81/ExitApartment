using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SkinZombie : Mob
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

    
    private Animator anim;

    [Header("스텝사운드컨트롤러"),SerializeField]
    private SoundController stepSoundCtr;
    private Transform target;
    private SoundController soundCtr;
    [SerializeField]
    private SoundController chaseSoundCtr;
    [Header("데드뷰"), SerializeField]
    private Transform deadView;

    private LayerMask layer = 1 << 7 | 1 << 6;
    void Start()
    {
        Init();
        anim = transform.GetComponent<Animator>();
        stepSoundCtr.AudioPath = GameManager.Instance.soundMgr.SoundList[106];


        agent = GetComponent<NavMeshAgent>();
        timer = waitTime;
        eEnemyState = EenemyState.Idle;

        soundCtr = GetComponent<SoundController>();
        soundCtr.AudioPath = GameManager.Instance.soundMgr.SoundList[71];
        chaseSoundCtr.AudioPath = GameManager.Instance.soundMgr.SoundList[72];
        soundCtr.Play();

        //soundCtr.StartCoroutine(soundCtr.PlayingRandomTimeSound(8f, 14f));
    }

    
    void Update()
    {
        anim.SetFloat("Speed", agent.speed);
        //if (EenemyState.None == eEnemyState)
        //    return;

        target = unitMgr.MobCtr.GetOverlaptarget(transform, hearRadius, layer);
        if(target != null)
        {
            if(target.gameObject.layer == 7)
            {
                if (!target.parent.gameObject.GetComponent<AudioSource>().isPlaying)
                {
                    target = null;
                }
            }
            else
            {
                if (!target.gameObject.GetComponent<AudioSource>().isPlaying)
                {
                    target = null;
                }
            }

                            
        }


        if (EenemyState.None != eEnemyState && EenemyState.Attack != eEnemyState)
        {
            eEnemyState = target ? EenemyState.Chase : EenemyState.Patrol;

        }



        switch (eEnemyState)
        {
            case EenemyState.None:
                agent.speed = 0f;
                AnimatorStateInfo ani = anim.GetCurrentAnimatorStateInfo(0);
                if (ani.IsName("Attack"))
                {
                    if (ani.normalizedTime > 0.9f)
                        eEnemyState = EenemyState.Idle;
                }
                else
                {
                    return;
                }
                break;

            case EenemyState.Idle:
                
                eEnemyState = EenemyState.Patrol;
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
                agent.speed = 0f;
                eEnemyState = EenemyState.None;
                break;

            default: break;
        }




    }
    public override void OnContect()
    {
        if(eEnemyState == EenemyState.None) return;
        eEnemyState = EenemyState.Attack;        
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

    protected override void Init()
    {
        base.Init();
    }

    private void ZombieStepSound()
    {
        stepSoundCtr.Play();
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 6)
        {
            if (eEnemyState == EenemyState.None) return;
            eEnemyState = EenemyState.Attack;            
            soundCtr.AudioPath = GameManager.Instance.soundMgr.SoundList[73];
            soundCtr.Play();
            other.gameObject.SetActive(false);
        }
    }
}
