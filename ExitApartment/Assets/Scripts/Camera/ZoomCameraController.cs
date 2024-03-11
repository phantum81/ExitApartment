using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ZoomCameraController : MonoBehaviour
{
    


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
            GameManager.Instance.cameraMgr.StartCoroutine(GameManager.Instance.cameraMgr.ChangeCamera(_zoomCam));
            _zoomCam.transform.position = _target.position + _target.forward * _distance;
            
            

            _zoomCam.transform.LookAt(_target);

            _zoomCam.transform.rotation = Quaternion.Euler(_zoomCam.transform.rotation.eulerAngles.x, 180f, _zoomCam.transform.rotation.eulerAngles.z);


        }
        else
        {
            GameManager.Instance.cameraMgr.StartCoroutine(GameManager.Instance.cameraMgr.ChangeCamera(_mainCam));
        }

    }
}
