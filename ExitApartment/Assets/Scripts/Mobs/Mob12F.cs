using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob12F : MonoBehaviour, IEnemyContect
{
    private int igonorelayer = 1 << 8 | 1<<2;

    private Transform target;
    private EenemyState eEnemyState = EenemyState.None;

    private float chaseLimit = 3f;
    void Start()
    {
        
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
                GameManager.Instance.eventMgr.ChangePlayerState(EplayerState.None);
            }
        }


        if( eEnemyState == EenemyState.Chase)
        {
            if (GameManager.Instance.eventMgr.GetPlayerState() != EplayerState.Die)
            {
                
                GameManager.Instance.eventMgr.ChangePlayerState(EplayerState.MentalDamage);
            }
        }

    }

    public void OnContect()
    {
        GameManager.Instance.unitMgr.GetContectTarget(this.transform);
        GameManager.Instance.eventMgr.ChangePlayerState(EplayerState.Die);
    }

    private bool DetectedPlayer()
    {
        Ray ray = new Ray(transform.position, transform.forward);

        if(Physics.Raycast(ray, out RaycastHit _hit, 17f, ~igonorelayer))
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


    IEnumerator SaveTarget(Transform _target)
    {
        
        yield return new WaitForSeconds(3f);
        target = null;
    }


}
