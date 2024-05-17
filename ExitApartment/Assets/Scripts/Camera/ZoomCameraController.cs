using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.GraphicsBuffer;

public class ZoomCameraController : MonoBehaviour
{
    [Header("¼Óµµ"),SerializeField]
    private float speed = 2f;



    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void ZoomCamera( Camera _zoomCam, Camera _mainCam ,Transform _target, float _distance)
    {
        if(!_zoomCam.enabled)
        {
            GameManager.Instance.cameraMgr.ChangeCamera(_zoomCam);
            _zoomCam.transform.position = _target.position - _target.forward * _distance;
            _zoomCam.transform.rotation = _target.rotation;
                  
        }
        else
        {
            GameManager.Instance.cameraMgr.ChangeCamera((_mainCam));
            
        }

    }


    public IEnumerator ZoomMove(Camera _zoomCam , Collider _col, float _dis)
    {
        Vector3 dir = Vector3.zero;
        Vector3 moveDir = Vector3.zero;
        InputManager inputMgr = GameManager.Instance.inputMgr;


        while (_zoomCam.enabled)
        {
            dir = new Vector3(inputMgr.InputDir.x, inputMgr.InputDir.z, 0f);
            dir.Normalize();

            Vector3 colCenter = _col.bounds.center;
            Vector3 colExtents = _col.bounds.extents;

            float minLocalX = Mathf.Clamp(_zoomCam.transform.localPosition.x, colCenter.x - colExtents.x, colCenter.x + colExtents.x);
            float minLocalY = Mathf.Clamp(_zoomCam.transform.localPosition.y, colCenter.y - colExtents.y, colCenter.y + colExtents.y);
            float minLocalZ = Mathf.Clamp(_zoomCam.transform.localPosition.z, colCenter.z - colExtents.z, colCenter.z + colExtents.z);

            _zoomCam.transform.position = new Vector3(minLocalX, minLocalY, minLocalZ) + _col.transform.forward *_dis ;
            
            _zoomCam.transform.Translate(dir * Time.deltaTime * speed, Space.Self);



            yield return null;
        }



    }
}
