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
        //�ϴ� ������ ���� ������ �ΰ��� ȹ��
    }
    public virtual void OnRayOut()
    {

    }
    public EInteractionType OnGetType()
    {
        return EInteractionType.Use;
    }
}
