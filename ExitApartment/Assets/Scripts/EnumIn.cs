using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EplayerState
{
    Stand,
    Walk,
    Run,
    Fall,
    Damaged,

};
public enum EcameraState
{
    FollowStand,
    FollowWalk,
    FollowRun,
    FollowGravity
};
public enum EstageEventState
{
    None,
    GravityReverse
};
