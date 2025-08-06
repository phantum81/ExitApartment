using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WineItem : Item
{
    EFloorType eFloorType = EFloorType.Home15EB;
    
    public override void Init()
    {

        base.Init();
        eItemType = EItemType.Wine;
        
    }



    public override void OnInteraction(Vector3 _angle)
    {
        base.OnInteraction(_angle);

    }


    public override void OnUseItem()
    {
        base.OnUseItem();
        

    }

    public override EInteractionType OnGetType()
    {
        return EInteractionType.Pick;
    }

    public override void OnThrowItem(float _time)
    {
        base.OnThrowItem(_time);
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

    public override void InitPosition()
    {

        base.InitPosition();
    }
    public override IEnumerator CoInitPosition()
    {
        isCoPlaying = true;
        bool isplay = true;
        while (true)
        {
            if (EFloorType.Home15EB == GameManager.Instance.unitMgr.ElevatorCtr.eCurFloor && !isplay)
            {
                InitPosition();
                isplay = true;
            }

            if(EFloorType.Home15EB != GameManager.Instance.unitMgr.ElevatorCtr.eCurFloor)
            {
                isplay = false;
            }


            yield return null;

        }
    }
}
