using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTable : ZoomableItem
{

    public override void Init()
    {
        base.Init();
    }

    public override void OnInteraction(Vector3 _angle)
    {
        base.OnInteraction(_angle);
        GameManager.Instance.ZoomMove(GameManager.Instance.cameraMgr.CameraDic[2], limitCol, zoomValue);
    }
    public override EInteractionType OnGetType()
    {
        return base.OnGetType();
    }
}
