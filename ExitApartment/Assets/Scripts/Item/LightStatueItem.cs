using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightStatueItem : MonoBehaviour, IInteraction
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
        

    }
    public void OnRayOut()
    {
        curMaterial.color = originColor;
    }

    public void OnGravity()
    {
        GameManager.Instance.unitMgr.OnChangeGravity(rigd, GameManager.Instance.unitMgr.ReserveGravity);
    }

    private void OnTriggerEnter(Collider other)
    {
        IEventContect col = other.GetComponent<IEventContect>();
        col?.OnContect(ESOEventType.OnClear12F);
    }

}
