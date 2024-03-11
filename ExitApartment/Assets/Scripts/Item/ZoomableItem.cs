using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomableItem : MonoBehaviour, IInteraction
{
    private Transform ray_collider;
    [Header("¡‹ ¡§µµ"), SerializeField]
    private float zoomValue = 0.5f;
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

    }

    public void OnRayOut()
    {

    }
}
