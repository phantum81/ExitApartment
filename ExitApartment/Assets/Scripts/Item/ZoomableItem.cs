using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomableItem : MonoBehaviour, IInteraction
{
    private Transform ray_collider;
    [Header("줌 정도"), SerializeField]
    private float zoomValue = 0.5f;
    [Header("이동 제한 콜라이더"), SerializeField]
    private Collider limitCol;



    public EZoomType eZoomType;
    public void Init()
    {
        ray_collider = transform.GetChild(0).transform;
    }

    public void OnRayHit(Color _color)
    {

    }
    public void OnInteraction(Vector3 _angle)
    {
        GameManager.Instance.ZoomCamera(GameManager.Instance.cameraMgr.CameraDic[2], GameManager.Instance.cameraMgr.CameraDic[0] , ray_collider, zoomValue);
        switch (eZoomType)
        {
            case EZoomType.Item:
                GameManager.Instance.ZoomMove(GameManager.Instance.cameraMgr.CameraDic[2], limitCol, zoomValue);
                break;
            case EZoomType.HomeTrap:
                break;
        }
    }

    public void OnRayOut()
    {

    }
}
