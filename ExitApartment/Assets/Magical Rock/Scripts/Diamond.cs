using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : MonoBehaviour, IInteraction
{

    
    public void Init()
    {

    }

    public virtual void OnRayHit(Color _color)
    {


    }
    public virtual void OnInteraction(Vector3 _angle)
    {
        transform.gameObject.SetActive(false);
        //ÇÏ´Ã »¡°²°Ô ºûµµ »¡°²°Ô ÀÎ°£¼º È¹µæ
    }
    public virtual void OnRayOut()
    {

    }
    public EInteractionType OnGetType()
    {
        return EInteractionType.Use;
    }
}
