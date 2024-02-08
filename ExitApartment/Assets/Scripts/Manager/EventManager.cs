using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    

    public EstageEventState eStageState = EstageEventState.None;
    public ESOEventType eCurEvent = ESOEventType.OnGravity;


    private void Awake()
    {
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 스테이지 상태변경
    /// 0: none 1: Eventing
    /// </summary>
    public void ChangeStageState(int _state)
    {
        switch (_state)
        {
            case 0:
                eStageState = EstageEventState.None;
                break;
            case 1:
                eStageState = EstageEventState.Eventing;
                break;


        }
    }
    public void ChangeEventType(int _state)
    {
        switch (_state)
        {
            case 0:
                eCurEvent = ESOEventType.OnGravity;
                break;
            case 1:
                eCurEvent = ESOEventType.OnDie12F;
                break;


        }
    }



}
