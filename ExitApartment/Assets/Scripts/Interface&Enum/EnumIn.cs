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
    Idle,
    Patrol,
    Chase,
    Attack,
    RunAway
}


public enum EElevatorButtonType
{
    Open,
    Close,
    Move,
};

public enum EElevatorWork
{
    Opening,
    Closing,
    Locking,

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
public enum EEscapeRoomEvent
{
    None,
    FakeRoom,
    PinkGarden,
    ExitRoom,
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
    OnMagicStone,
    
    
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


    One,
    Two,
    Three,
    Four,
    Five,
    Six,
    Seven,
    Eight,
    Nine,

    Inventory,


}

public enum EgameState
{
    Menu,
    InGame,
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


public enum EMobType
{
    Mob12F,
    Pumpkin,
    SkinLess,
    Crab,
    Bat,

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
    CurBoard,
}


public enum EUiObjectType
{
    Inventory,
    PickMark,
}

public enum ENoteType
{
    Pumpkin,
    Forest,
    Mob12F,
    Last,

}

public enum EInteractionType
{
    Pick,
    See,
    Use,
    Find,
    Pull,
    Push,
    Open,
    Close,
    Press,
    Write,
}


public enum EFloorState
{
    Open,
    Close,
}


public enum EFloorType
{
    Home15EB,
    Nothing436A, 
    Mob122F,
    Forest5ABC,
    Escape888B,

}


public enum ESoundType
{
    Bgm,
    Effect,
   

}

public enum EDoorType
{
    Closet,
    HomeDoor,
    Locked,
}

public enum ESeePoint
{
    Pumpkin,
    Bat,
}