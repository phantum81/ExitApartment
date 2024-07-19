using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterItem : Item
{
    public override void Init()
    {

        base.Init();
        eItemType = EItemType.Water;
    }



    public override void OnInteraction(Vector3 _angle)
    {
        base.OnInteraction(_angle);

    }


    public override void OnUseItem()
    {



    }

    public override void OnThrowItem()
    {
        base.OnThrowItem();
    }
    public override EInteractionType OnGetType()
    {
        return EInteractionType.Pick;
    }

    public override void OnGravityChange()
    {
        base.OnGravityChange();
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }
    protected override void OnCollisionEnter(Collision other)
    {
        base.OnCollisionEnter(other);
    }
}
