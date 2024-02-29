using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightStatueItem : MonoBehaviour, IInteraction, IUseItem
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

    }

    public void OnThrowItem()
    {

    }


    public void OnGravity()
    {
        GameManager.Instance.unitMgr.OnChangeGravity(rigd, GameManager.Instance.unitMgr.ReserveGravity,5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        IEventContect col = other.GetComponent<IEventContect>();
        col?.OnContect(ESOEventType.OnClear12F);
    }

}
