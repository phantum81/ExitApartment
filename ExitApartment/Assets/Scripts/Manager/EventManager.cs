using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    

    public EstageEventState eStageState = EstageEventState.None;
    public ESOEventType eCurEvent = ESOEventType.OnGravity;



    [SerializeField]
    private PlayerStateHFSMMachine playerStateHFSM;

    private bool isPumpkinEvent = false;
    
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
    /// �������� ���º���
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
    /// <summary>
    /// �̺�Ʈ Ÿ�� 0: �׷���Ƽ �� 1: 12����� 2: 12�� Ŭ����
    /// </summary>
    /// <param name="_state"></param>
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
            case 2:
                eCurEvent = ESOEventType.OnClear12F;
                break;
            //case 3:
            //    eCurEvent= ESOEventType.OnHomeTrap;
            //    break;


        }
    }


    public void ChangePlayerState(EplayerState _state)
    {
        playerStateHFSM.ChangePlayerState(_state);
    }

    public EplayerState GetPlayerState()
    {
        return playerStateHFSM.EPlayerCurState;
    }

   public void SetIsPumpkinEvent(bool _bool)
   {
        isPumpkinEvent = _bool;
   }

   public bool GetIsPumpkinEvent()
   {
        return isPumpkinEvent;
   }
}
