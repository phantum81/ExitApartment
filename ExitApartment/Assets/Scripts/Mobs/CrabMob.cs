using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CrabMob : Mob
{
    
    [Header("대기시간"), SerializeField]
    private float waitTime;

    [Header("탐색반경"), SerializeField]
    private float lookRadius;

    [Header("시야반경"), SerializeField]
    private float viewAngle;

    private SoundController soundCtr;

    private float timer;
    private NavMeshAgent agent;
    private SoundManager soundMgr;
    private Animator anim;
    
    
    private Transform target;
    private Vector3 origin;
    private bool isEvent=false;
    [Header("데드뷰"), SerializeField]
    private Transform deadView;
    private CameraController cameraCtr;

    private Coroutine coShakeRoutine;


    void Start()
    {
        Init();

        anim = transform.GetComponent<Animator>();
        soundCtr = GetComponent<SoundController>();
        soundMgr = GameManager.Instance.soundMgr;
        agent = GetComponent<NavMeshAgent>();
        timer = waitTime;
        eEnemyState = EenemyState.None;
        origin = transform.position;
        soundCtr.AudioPath = soundMgr.SoundList[105];
        cameraCtr = GameManager.Instance.cameraMgr.CameraCtr;



    }
    void Update()
    {
        if (!isEvent) return;

        

        anim.SetFloat("Speed", agent.speed);
        UpdateTarget();

        if (eEnemyState != EenemyState.Attack || eEnemyState != EenemyState.None)
        {
            UpdateState();
        }

        PerformActionBasedOnState();
    }


    void UpdateTarget()
    {
        target = unitMgr.MobCtr.GetLookingTarget(transform, lookRadius, 1 << 7);
        if (target && eEnemyState == EenemyState.None)
            eEnemyState = EenemyState.Idle;
    }

    void UpdateState()
    {

        if (unitMgr.MobCtr.CheckTargetInSight(transform, target, viewAngle))
        {
            eEnemyState = EenemyState.Chase;
        }
        else if (!target && eEnemyState != EenemyState.None)
        {
            eEnemyState = EenemyState.Idle;
        }
    }

    void PerformActionBasedOnState()
    {
        switch (eEnemyState)
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
                default: break;
        }
    }

    void HandleIdleState()
    {
        agent.speed = 0f;
        anim.SetBool("Interact", true);
    }

    void HandleChaseState()
    {
        if (!target) return;

        agent.SetDestination(target.position);
        agent.speed = 5f;
    }

    void HandleAttackState()
    {
        anim.CrossFade("Attack", 0.1f);
        eEnemyState = EenemyState.None;
    }



    public override void OnContect()
    {
        if(eEnemyState != EenemyState.None)
        {
            eEnemyState = EenemyState.Attack;
            
            GameManager.Instance.unitMgr.SetContectTarget(deadView);
            eventMgr.ChangePlayerState(EplayerState.Die);
            eEnemyState = EenemyState.None;
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
        if (target)
        {
            Gizmos.DrawRay(transform.position, (target.position - transform.position).normalized);
            Gizmos.DrawLine(transform.position, target.position);
           
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 9)
        {
            isEvent = false;
            agent.SetDestination(origin);


        }
    }

    protected override void Init()
    {
        base.Init();
    }

    private void CrabStepSound()
    {
        soundCtr.Play();
        
    }
    private void CrabCameraShake()
    {
        float shakeAmount = 0f;
        float minDis = 1f;
        float maxDis = 39f;
        float minAmount = 0.02f;
        float maxAmount = 0.2f;
        if (coShakeRoutine!= null)
        {
            return;
        }

        if(target != null)
        {
            
            float dis = Vector3.Distance(target.position, transform.position);
            float normalize = 1- Mathf.InverseLerp(minDis, maxDis, dis);

            shakeAmount = Mathf.Lerp(minAmount, maxAmount, Mathf.Clamp01(normalize));
        }


        coShakeRoutine = StartCoroutine(CoCameraShake(shakeAmount));

    }

    private IEnumerator CoCameraShake(float _shakeAmount)
    {
        

        yield return cameraCtr.CameraShake(GameManager.Instance.cameraMgr.CameraDic[0], 0.5f, _shakeAmount);

        coShakeRoutine = null;
    }

}
