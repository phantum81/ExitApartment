using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WaterItem : Item
{
    
    [Header("효과수치(스피드곱)"), SerializeField]
    private float multiply;
    [Header("지속시간"), SerializeField]
    private float usingTime;
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
        base.OnUseItem();
        GameManager.Instance.itemMgr.ChangeSpeed(multiply, usingTime);

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
