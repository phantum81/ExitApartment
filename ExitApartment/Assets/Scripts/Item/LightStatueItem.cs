using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightStatueItem : Item
{

    public override void Init()
    {
        base.Init();
    }

    public override void OnRayHit(Color _color)
    {
        base.OnRayHit(_color);

    }
    public override void OnInteraction(Vector3 _angle)
    {
        base.OnInteraction(Vector3.zero);

    }
    public override void OnRayOut()
    {
        base.OnRayOut();
    }

    public override void OnUseItem()
    {

        Color curColor;
        Light myLight;
        myLight = transform.GetComponentInChildren<Light>();
        for (int i = 0; i < transform.childCount + 1; i++)
        {
            curColor = transform.GetComponentsInChildren<Renderer>()[i].material.GetColor("_EmissionColor");
            transform.GetComponentsInChildren<Renderer>()[i].material.SetColor("_EmissionColor", curColor == Color.black ? Color.white : Color.black);
        }
        myLight.enabled = !myLight.enabled;

    }

    public override void OnThrowItem()
    {
        base.OnThrowItem();
    }


    public override void OnGravityChange()
    {
        base.OnGravityChange();
    }

    private void OnTriggerEnter(Collider other)
    {
        IEventContect col = other.GetComponent<IEventContect>();
        col?.OnContect(ESOEventType.OnClear12F);
        
    }

}
