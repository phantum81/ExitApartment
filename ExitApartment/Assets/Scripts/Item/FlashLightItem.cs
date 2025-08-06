using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLightItem : Item
{
    private GameObject lightGo;

    private EFloorType eFloorType = EFloorType.Home15EB;

    public override void Init()
    {
        base.Init();
        eItemType = EItemType.FlashLight;
        lightGo = transform.GetComponentInChildren<Light>().gameObject;
        lightGo.SetActive(false);
    }

    public override void OnRayHit(Color _color)
    {
        base.OnRayHit(_color);

    }
    public override void OnInteraction(Vector3 _angle)
    {
        base.OnInteraction(new Vector3(90f,0f,0f));

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

        lightGo.SetActive(!lightGo.activeSelf);

    }

    public override void OnThrowItem(float _time)
    {
        base.OnThrowItem(_time);
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

    public override void InitPosition()
    {
        base.InitPosition();
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
