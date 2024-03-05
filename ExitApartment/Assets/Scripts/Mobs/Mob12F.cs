using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob12F : MonoBehaviour, IEnemyContect
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (DetectedPlayer())
        {
            if(GameManager.Instance.eventMgr.GetPlayerState() != EplayerState.Die)
                GameManager.Instance.eventMgr.ChangePlayerState(EplayerState.MentalDamage);
            
        }
        else
        {
            GameManager.Instance.eventMgr.ChangePlayerState(EplayerState.None);

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

        if(Physics.Raycast(ray, out RaycastHit _hit, 15f))
        {
            return false;
        }
        else if(Physics.Raycast(ray, out RaycastHit _hitPlayer, 15f, 1 << 7))
        {
            return true;
        }
        else
            return false;
    }


}
