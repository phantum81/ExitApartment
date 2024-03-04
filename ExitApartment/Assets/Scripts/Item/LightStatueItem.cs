using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightStatueItem : MonoBehaviour, IInteraction, IUseItem, IGravityChange
{
    private Color originColor;
    private Material curMaterial;
    private Rigidbody rigd;
 


    public void Init()
    {
        GameManager.Instance.itemMgr.InitInteractionItem(out curMaterial, out originColor, transform);
        rigd = GetComponent<Rigidbody>();
    }

    public void OnRayHit(Color _color)
    {
        curMaterial.color = _color;

    }
    public void OnInteraction()
    {
        GameManager.Instance.itemMgr.PickItem(this.transform);

    }
    public void OnRayOut()
    {
        curMaterial.color = originColor;
    }

    public void OnUseItem()
    {
        //curMaterial.EnableKeyword("_EMISSON");
        //curMaterial.globalIlluminationFlags = MaterialGlobalIlluminationFlags.RealtimeEmissive;
        Color curColor;
        Light myLight;
        myLight = transform.GetComponentInChildren<Light>();
        for (int i =0; i< transform.childCount+1; i++)
        {
            curColor = transform.GetComponentsInChildren<Renderer>()[i].material.GetColor("_EmissionColor");
            transform.GetComponentsInChildren<Renderer>()[i].material.SetColor("_EmissionColor", curColor == Color.black ? Color.white : Color.black);
        }
        myLight.enabled = !myLight.enabled;
        
    }

    public void OnThrowItem()
    {
        GameManager.Instance.itemMgr.ThrowItem(this.transform);
    }


    public void OnGravityChange()
    {
        GameManager.Instance.unitMgr.OnChangeGravity(rigd, GameManager.Instance.unitMgr.ReserveGravity,5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        IEventContect col = other.GetComponent<IEventContect>();
        col?.OnContect(ESOEventType.OnClear12F);
        
    }

}
