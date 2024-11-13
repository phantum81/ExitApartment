using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeItem : Item
{
    public override void Init()
    {

        base.Init();
        eItemType = EItemType.Rope;
    }



    public override void OnInteraction(Vector3 _angle)
    {
        base.OnInteraction(_angle);

    }
    public override EInteractionType OnGetType()
    {
        return EInteractionType.Pick;
    }

    public override void OnUseItem()
    {



    }

    public override void OnThrowItem(float _time)
    {
        base.OnThrowItem(_time) ;
    }


    public override void OnGravityChange()
    {
        base.OnGravityChange();
    }
}
