using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparrowBottleItem : Item
{
    private EFloorType eFloorType = EFloorType.Home15EB;
    
    private Vector3 originParentPos ; 
    public override void Init()
    {
        
        base.Init();
        eItemType = EItemType.Bottle;
        
        originParentPos = parent.position;
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
    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);


    }

    public void InitPosition()
    {
        if (transform.position != originPos && transform.parent == null)
        {
            if(parent.position != originParentPos)
            {
                Vector3 distance = parent.position - originParentPos;
                transform.position = originPos;
                transform.position += distance;
            }
            else
                transform.position = originPos;

            transform.parent = parent;
            transform.rotation = originRotate;            
            transform.GetComponent<Collider>().enabled = true;
        }
    }
    public override IEnumerator CoInitPosition()
    {
        isCoPlaying = true;
        bool isplay = false;
        while (true)
        {
            if (EFloorType.Home15EB == GameManager.Instance.unitMgr.ElevatorCtr.eCurFloor && !isplay)
            {
                InitPosition();
                isplay = true;
            }

            if (EFloorType.Home15EB != GameManager.Instance.unitMgr.ElevatorCtr.eCurFloor)
            {
                isplay = false;
            }
            yield return null;

        }
    }


}
