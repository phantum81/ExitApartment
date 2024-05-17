using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomableItem : MonoBehaviour, IInteraction
{
    [SerializeField]
    protected Transform ray_collider;
    [Header("�� ����"), SerializeField]
    protected float zoomValue = 0.5f;
    [Header("�̵� ���� �ݶ��̴�"), SerializeField]
    protected Collider limitCol;



    //public EZoomType eZoomType;
    public virtual void Init()
    {
        ray_collider = transform.GetChild(0).transform;
    }

    public void OnRayHit(Color _color)
    {

    }
    public virtual void OnInteraction(Vector3 _angle)
    {
        GameManager.Instance.ZoomCamera(GameManager.Instance.cameraMgr.CameraDic[2], GameManager.Instance.cameraMgr.CameraDic[0] , ray_collider, zoomValue);
    }

    public void OnRayOut()
    {

    }
}
