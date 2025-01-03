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


    private void OnTriggerEnter(Collider other)
    {

        ISOEventContect col = other.GetComponent<ISOEventContect>();
        IEnemyContect ecol = other.GetComponent<IEnemyContect>();
        IContect ncol = other.GetComponent<IContect>();
        col?.OnContect(ESOEventType.OnGravity);
        col?.OnContect(ESOEventType.OnDie12F);
        ecol?.OnContect();
        ncol?.OnContect();
    }


    private void OnTriggerExit(Collider other)
    {
        IContect ncol = other.GetComponent<IContect>();
        ncol?.OnExit();
    }


    private void OnTriggerStay(Collider other)
    {
        IContect ncol = other.GetComponent<IContect>();
        ncol?.OnStay();
    }





    public void OnGravityChange()
    {
        playerCtr.ChangeGravity();
    }
    public void OnDead12F()
    {
        playerCtr.Rigd.useGravity = false;
        playerCtr.Player.GetComponent<Collider>().enabled = false;
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if(Input.GetKey(KeyCode.P))
    //    {
    //        Debug.Log(collision.gameObject.name);
    //    }
    //}
}
