using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class HFSM<TState> where TState : struct, System.Enum
{
    private static HFSM<TState> instance;
    public static HFSM<TState> Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new HFSM<TState>();
            }
            return instance;
        }
    }

    private TState curState;
    private Dictionary<TState, IState> stateDic;

    private HFSM()
    {
        curState = default(TState);
        stateDic= new Dictionary<TState, IState>();
        Init();
        EnterState(curState);

    }

    public void Init()
    {
        foreach(TState state in Enum.GetValues(typeof(TState)))
        {
            stateDic[state] = CreateStateInstance(state);
        }
    }
    private IState CreateStateInstance(TState _state)
    {
        switch(_state)
        {
            //case EplayerMoveState.None:
            //    //return new PlayerNone<PlayerController>();
            //    break;
            default:
            return null;
        }
    }


    public void ChangeState(TState _newState)
    {
        ExitState(curState);
        curState = _newState;
        EnterState(curState);
    }

    private void EnterState(TState _state)
    {

    }
    private void ExitState(TState _state)
    {

    }

    public void Update()
    {

    }

}
