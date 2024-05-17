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
public enum EenemyState
{
    None,
    Patrol,
    Chase,
}


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
/// �̺�Ʈ ���� 0 : �׷���Ƽ, 1 : 12����� 2:12�� ���� 
/// </summary>
public enum ESOEventType
{
    OnGravity,
    OnDie12F,
    OnClear12F,
    
    
};

/// <summary>
/// Ű �Է� 0: �� 1:�� 2:�� 3:�� 4:�޸��� 5: ��ȣ�ۿ� 6: ������/���� 7: �����ۻ�� 8:ui�޴�
/// </summary>
public enum EuserAction
{
    MoveForward,
    MoveBackward,
    MoveRight,
    MoveLeft,
    Run,

    Interaction,
    Throw,
    UseItem,

    Ui_Menu,

}



public enum EpostProcessType
{
    Vignette,
    Grain,
    LensDistortion,
    ChromaticAberration,
}



public enum ETestFloor
{
    Home,
    Monster,
    None
}



public enum EZoomType
{
    Item,
    HomeTrapDoor,
}


public enum EItemType
{
    None,
    Rope,
    Bottle,
    FlashLight,
    LightStatue,
    Water,
    Wine,
}