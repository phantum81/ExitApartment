using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLightItem : Item
{
    private GameObject lightGo;



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
    public override void OnRayOut()
    {
        base.OnRayOut();
    }

    public override void OnUseItem()
    {

        lightGo.SetActive(!lightGo.activeSelf);

    }

    public override void OnThrowItem()
    {
        base.OnThrowItem();
    }


    public override void OnGravityChange()
    {
        base.OnGravityChange();
    }
}
