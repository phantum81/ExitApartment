using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob12F : MonoBehaviour, IEnemyContect
{
    private int igonoreLayer = 1 << 8 | 1<<2;

    private Transform target;
    private EenemyState eEnemyState = EenemyState.None;
    private EventManager eventMgr;
    private float chaseLimit = 3f;
    void Start()
    {
        eventMgr = GameManager.Instance.eventMgr;
    }

    // Update is called once per frame
    void Update()
    {
        if(target != null)
        {
            eEnemyState = EenemyState.Chase;
        }
        else
            eEnemyState = EenemyState.None;
        // 자세한 상태 수정이 이후필요


        if (DetectedPlayer())
        {
            chaseLimit = 3f;
        }
        else
        {
            chaseLimit -= Time.deltaTime;
            if(chaseLimit < 0)
            {
                target = null;
                eventMgr.ChangePlayerState(EplayerState.None);
            }
        }

        
        if( eEnemyState == EenemyState.Chase && eventMgr.GetPlayerState() != EplayerState.Die)
        {
            eventMgr.ChangePlayerState(EplayerState.MentalDamage);
        }

    }

    public void OnContect()
    {
        GameManager.Instance.unitMgr.GetContectTarget(this.transform);
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




}
