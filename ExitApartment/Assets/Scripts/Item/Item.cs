using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, IInteraction, IUseItem, IGravityChange
{
    protected List<Color> originColor = new List<Color>();
    protected List<Material> curMaterial = new List<Material>();
    protected Rigidbody rigd;



    public virtual void Init()
    {
        GameManager.Instance.itemMgr.InitInteractionItem(ref curMaterial, ref originColor, transform);
        rigd = GetComponent<Rigidbody>();
    }

    public virtual void OnRayHit(Color _color)
    {
        foreach (Material mat in curMaterial)
        {
            mat.color = _color;
        }

    }
    public virtual void OnInteraction(Vector3 _angle)
    {
        GameManager.Instance.itemMgr.PickItem(this.transform, _angle);

    }
    public virtual void OnRayOut()
    {
        for (int i = 0; i < curMaterial.Count; i++)
        {
            curMaterial[i].color = originColor[i];
        }
    }

    public virtual void OnUseItem()
    {



    }

    public virtual void OnThrowItem()
    {
        GameManager.Instance.itemMgr.ThrowItem(this.transform);
    }


    public virtual void OnGravityChange()
    {
        GameManager.Instance.unitMgr.OnChangeGravity(rigd, GameManager.Instance.unitMgr.ReserveGravity, 9f);
    }


}
