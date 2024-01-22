using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EplayerMoveState
{
    
    Stand,
    Walk,
    Run,
    Fall,
    None,

};
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
};

public enum EplayerState
{
    None,
    MentalDamage,
    Damage,
    Die,
};
