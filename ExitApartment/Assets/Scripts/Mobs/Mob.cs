using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob : MonoBehaviour, IEnemyContect
{
    protected EenemyState eEnemyState = EenemyState.Idle;
    protected EventManager eventMgr;
    protected UnitManager unitMgr;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void OnContect()
    {

    }

    protected virtual void Init()
    {
        eventMgr = GameManager.Instance.eventMgr;
        unitMgr= GameManager.Instance.unitMgr;
    }

}
