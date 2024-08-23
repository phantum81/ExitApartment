using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob12F : Mob
{
    private int igonoreLayer = 1 << 8 | 1<<2;

    private Transform target;
    
    private EMobType eMobType = EMobType.Mob12F;
   
    private float chaseLimit = 3f;
    [Header("µ¥µåºä"),SerializeField]
    private Transform deadView;



    void Start()
    {
        Init();
        eEnemyState= EenemyState.None;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (eventMgr.GetPlayerState() == EplayerState.Die)
            return;

        if (DetectedPlayer())
        {
            chaseLimit = 3f;
            eEnemyState = EenemyState.Chase;
            eventMgr.ChangePlayerState(EplayerState.MentalDamage);
        }
        

    }

    public override void OnContect()
    {
        GameManager.Instance.unitMgr.SetContectTarget(deadView);

        eventMgr.ChangePlayerState(EplayerState.Die);
    }

    private bool DetectedPlayer()
    {
        Ray ray = new Ray(transform.position, transform.forward);

        if(Physics.Raycast(ray, out RaycastHit _hit, 17f, ~igonoreLayer))
        {
            if(_hit.transform.gameObject.layer == 7)
            {
                target = _hit.transform;
                return true;
            }
            else
                return false;
        }
        else
            return false;
    }

    public void OnDrawGizmos()
    {
        Debug.DrawRay(transform.position, transform.forward * 17f, Color.green, 0.2f);
    }

    protected override void Init()
    {
        base.Init();
    }
}
