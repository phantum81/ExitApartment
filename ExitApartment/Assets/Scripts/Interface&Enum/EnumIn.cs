using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �÷��̾� ���� 0:Stand 1:Walk 2:Run 3:Fall 4:None
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
/// ī�޶� ���� 0:Stand 1:Walk 2:Run 3:Gravity 4:None
/// </summary>
public enum EcameraState  // ĳ���� �����ӿ� ���� �޶���.
{
    FollowStand,
    FollowWalk,
    FollowRun,
    FollowFall,
    None,
};

/// <summary>
/// �������� ���� 0:None  1:Eventing
/// </summary>
public enum EstageEventState
{
    None,
    Eventing,
};


/// <summary>
/// �÷��̾� ���� 0:None 1:MentalDamage 2:Damage 3:Die
/// </summary>
public enum EplayerState  // ���� �̺�Ʈ������ �Ͼ.
{
    None,
    MentalDamage,
    Damage,
    Die,
};


/// <summary>
/// �̺�Ʈ ���� 0 : �׷���Ƽ, 1 : 12����� 
/// </summary>
public enum ESOEventType
{
    OnGravity,
    OnDie12F,
};




