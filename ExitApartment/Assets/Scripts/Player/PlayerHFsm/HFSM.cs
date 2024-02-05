using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class HFSM<TState, T> where TState : System.Enum where T : MonoBehaviour
{
    private static HFSM<TState, T> instance;
    public static HFSM<TState, T> Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new HFSM<TState, T>();
            }
            return instance;
        }
    }

    private TState curState;
    public TState CurState => curState;
    private Dictionary<TState, IState<T>> stateDic;

    private HFSM()
    {
        curState = default(TState);
        stateDic= new Dictionary<TState, IState<T>>();
        Init();
        

    }

    public void Init()
    {
        foreach(TState state in Enum.GetValues(typeof(TState)))
        {
            stateDic[state] = CreateStateInstance(state);
        }
    }
    private IState<T> CreateStateInstance(TState _state)
    {
        switch(_state)
        {
            case EplayerMoveState.None:
                return new PlayerNone<T>();
            case EplayerMoveState.Stand:
                 return new PlayerStand<T>();
            case EplayerMoveState.Walk:
                return new PlayerWalk<T>();
            case EplayerMoveState.Run:
                return new PlayerRun<T>();
            case EstageEventState.None:
                
            //case EstageEventState.GravityReverse:

            //case EstageEventState.Die12F:

            case EstageEventState.Eventing:


            default:
            return null;
        }
    }


    public void ChangeState(TState _newState, T _obj)
    {
        ExitState(curState, _obj);
        curState = _newState;
        EnterState(curState, _obj);
    }

    private void EnterState(TState _state, T _obj)
    {
        stateDic[_state].OperateEnter(_obj);
    }
    private void ExitState(TState _state, T _obj)
    {
        stateDic[_state].OperateExit(_obj);
    }

    public void Update(T _obj)
    {
        stateDic[curState].OperateUdate(_obj);
    }

}
