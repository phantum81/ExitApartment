using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob12F : MonoBehaviour, IEnemyContect
{
    private int igonoreLayer = 1 << 8 | 1<<2;

    private Transform target;
    private EenemyState eEnemyState = EenemyState.None;
    private EMobType eMobType = EMobType.Mob12F;
    private EventManager eventMgr;
    private float chaseLimit = 3f;

    void Start()
    {
        eventMgr = GameManager.Instance.eventMgr;
    }

    // Update is called once per frame
    void Update()
    {


        if (DetectedPlayer() && eventMgr.GetPlayerState() != EplayerState.Die)
        {
            chaseLimit = 3f;
            eEnemyState = EenemyState.Chase;
            eventMgr.ChangePlayerState(EplayerState.MentalDamage);
        }
        else
        {
            chaseLimit -= Time.deltaTime;
            if(chaseLimit < 0)
            {
                if(target != null)
                {
                    target = null;
                    eEnemyState = EenemyState.None;
                    eventMgr.ChangePlayerState(EplayerState.None);
                }
            }
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

    public void OnDrawGizmos()
    {
        Debug.DrawRay(transform.position, transform.forward * 17f, Color.green, 0.2f);
    }


}
