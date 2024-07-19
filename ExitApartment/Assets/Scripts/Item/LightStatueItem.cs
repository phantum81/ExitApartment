using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightStatueItem : Item
{

    public override void Init()
    {
        base.Init();
        eItemType = EItemType.LightStatue;
    }

    public override void OnRayHit(Color _color)
    {
        base.OnRayHit(_color);

    }
    public override void OnInteraction(Vector3 _angle)
    {
        base.OnInteraction(Vector3.zero);

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

        Color curColor;
        Light myLight;
        Renderer[] ren = transform.GetComponentsInChildren<Renderer>();
        myLight = transform.GetComponentInChildren<Light>();
        // 자신의 Renderer를 제외한 리스트 생성
        List<Renderer> childRenderers = new List<Renderer>();
        foreach (Renderer renderer in ren)
        {
            if (renderer.gameObject != this.gameObject)
            {
                childRenderers.Add(renderer);
            }
        }

        for (int i = 0; i < childRenderers.Count ; i++)
        {
            Material mat = childRenderers[i].material;
            mat.EnableKeyword("_EMISSION");
            curColor = mat.GetColor("_EmissionColor");
            Color newColor = curColor == Color.black ? Color.white : Color.black;
            mat.SetColor("_EmissionColor", newColor);
            DynamicGI.SetEmissive(childRenderers[i], newColor);
            childRenderers[i].UpdateGIMaterials();
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

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        IEventContect col = other.GetComponent<IEventContect>();
        col?.OnContect(ESOEventType.OnClear12F);
        
    }


    protected override void OnCollisionEnter(Collision other)
    {
        base.OnCollisionEnter(other);
    }
}
