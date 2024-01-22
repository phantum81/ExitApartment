using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    

    public EstageEventState eStageState = EstageEventState.None;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeStageState(int _state)
    {
        switch (_state)
        {
            case 0:
                eStageState = EstageEventState.None;
                break;
            case 1:
                eStageState = EstageEventState.GravityReverse;
                break;
            case 2:
                eStageState = EstageEventState.Die12F;
                GameManager.Instance.unitMgr.PlayerCtr.OnDead12F();
                break;



        }
    }


    public void ChangeGravity(Rigidbody _rigd, Vector3 _gravity)
    {
        _rigd.useGravity = false;
        _rigd.AddForce(_gravity * 9.81f * _rigd.mass, ForceMode.Acceleration);


    }

    public void OnDead12F(UnityEvent _event)
    {
        _event.Invoke();
    }
}
