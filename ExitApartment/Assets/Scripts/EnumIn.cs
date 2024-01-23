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
/// <summary>
/// 카메라 상태 0:Stand 1:Walk 2:Run 3:Gravity 4:None
/// </summary>
public enum EcameraState
{
    FollowStand,
    FollowWalk,
    FollowRun,
    FollowGravity,
    None,
};

public enum EstageEventState
{
    None,
    GravityReverse,
    Die12F,
    Eventing,
};

public enum EplayerState
{
    None,
    MentalDamage,
    Damage,
    Die,
};
