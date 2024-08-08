using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparrowBottleItem : Item
{

    public override void Init()
    {
        
        base.Init();
        eItemType = EItemType.Bottle;
    }
    public override void OnRayHit(Color _color)
    {
        base.OnRayHit(_color);

    }


    public override void OnInteraction(Vector3 _angle)
    {
        base.OnInteraction(_angle);

    }
    public override EInteractionType OnGetType()
    {
        return EInteractionType.Pick;
    }


    public override void OnRayOut()
    {
        base.OnRayOut();
    }

    public override void OnUseItem()
    {

        

    }

    public override void OnThrowItem()
    {
        base.OnThrowItem();
    }


    public override void OnGravityChange()
    {
        base.OnGravityChange();
    }
    protected override void OnCollisionEnter(Collision other)
    {
        base.OnCollisionEnter(other);

    }
}
