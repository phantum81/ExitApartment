using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ZoomCameraController : MonoBehaviour
{
    [Header("속도"),SerializeField]
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
        Vector3 movePos = Vector3.zero;
        InputManager inputMgr = GameManager.Instance.inputMgr;
        while(_zoomCam.enabled)
        {
            dir = new Vector3(inputMgr.InputDir.x, inputMgr.InputDir.z, 0f);
            dir.Normalize();

            Vector3 _colSize = new Vector3(_col.bounds.size.x, _col.bounds.size.y, _zoomCam.transform.position.z);
            
            
            movePos = _zoomCam.transform.position + dir * Time.deltaTime * speed;



            Vector3 colCenter = _col.bounds.center;
            Vector3 colSize = _col.bounds.size;
            Vector3 colMin = colCenter - colSize / 2f;
            Vector3 colMax = colCenter + colSize / 2f;
            Vector3 newPosWithoutZ = new Vector3(movePos.x, movePos.y, 0f);

            // 이동할 위치가 Collider의 범위를 벗어나지 않는지 확인합니다.
            if (newPosWithoutZ.x >= colMin.x && newPosWithoutZ.x <= colMax.x &&
                newPosWithoutZ.y >= colMin.y && newPosWithoutZ.y <= colMax.y)
            {
                // Collider 내부에 위치할 경우에만 이동을 수행합니다.
                _zoomCam.transform.Translate(dir * Time.deltaTime * speed);

            }
            else if (newPosWithoutZ.x >= colMax.x ||
                newPosWithoutZ.y >= colMax.y)
            {
                _zoomCam.transform.Translate(-dir * Time.deltaTime * speed);
            }

            yield return null;
        }

    }
}
