using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColliderTrigger : MonoBehaviour, IGravityChange
{
    private PlayerController playerCtr;
    void Start()
    {
        playerCtr = GameManager.Instance.unitMgr.PlayerCtr;   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        
        IEventContect col = other.GetComponent<IEventContect>();
        IEnemyContect ecol = other.GetComponent<IEnemyContect>();
        col?.OnContect(ESOEventType.OnGravity);
        col?.OnContect(ESOEventType.OnDie12F);
        ecol?.OnContect();
    }

    public void OnGravityChange()
    {
        playerCtr.ChangeGravity();
    }
}
