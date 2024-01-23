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

    /// <summary>
    /// 스테이지 상태변경
    /// 0: none 1: gravityReverse 2: Die12F 3: Eventing
    /// </summary>
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
                break;
            case 3:
                eStageState = EstageEventState.Eventing;
                break;


        }
    }

    public void OnDead12F()
    {
        GameManager.Instance.unitMgr.PlayerCtr.OnDead12F();
    }




}
