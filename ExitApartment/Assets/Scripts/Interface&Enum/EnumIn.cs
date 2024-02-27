using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 플레이어 무브 0:Stand 1:Walk 2:Run 3:Fall 4:None
/// </summary>
public enum EplayerMoveState
{
    
    Stand,
    Walk,
    Run,
    Fall,
    None,

};

public enum EItemInteraction
{
    None,
    On,

};

public enum EElevatorButtonType
{
    Open,
    Close,

};

public enum EElevatorWork
{
    Opening,
    Closing,


};

/// <summary>
/// 카메라 상태 0:Stand 1:Walk 2:Run 3:Gravity 4:None
/// </summary>
public enum EcameraState  // 캐릭터 움직임에 따라 달라짐.
{
    FollowStand,
    FollowWalk,
    FollowRun,
    FollowFall,
    None,
};

/// <summary>
/// 스테이지 상태 0:None  1:Eventing
/// </summary>
public enum EstageEventState
{
    None,
    Eventing,
};


/// <summary>
/// 플레이어 상태 0:None 1:MentalDamage 2:Damage 3:Die
/// </summary>
public enum EplayerState  // 보통 이벤트때문에 일어남.
{
    None,
    MentalDamage,
    Damage,
    Die,
};


/// <summary>
/// 이벤트 종류 0 : 그래비티, 1 : 12층사망 
/// </summary>
public enum ESOEventType
{
    OnGravity,
    OnDie12F,
};




