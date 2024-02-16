using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraController : MonoBehaviour
{
    
    private Vector3 InGameCameraPos;
    [Header("카메라Z오프셋"),SerializeField]
    private float offSetZ = 2f;
    [Header("카메라X오프셋"), SerializeField]
    private float offSetX = 2f;
    [Header("카메라Y오프셋"),SerializeField]
    private float offSetY = 1f;
    [Header("민감도"),SerializeField]
    private float sensitivity = 2f;
    public float Sensitivity => sensitivity;

    [Header("회전속도"),SerializeField]
    private float rotSpeed = 1f;
    [Header("카메라 충돌 보정값"), SerializeField]
    private float cameraFix = 0.5f;
    [Header("카메라 충돌 러프값"), SerializeField]
    private float cameraFixLerp = 5f;

    //카메라 회전관련 각도설정
    private float maxXAngle = 70f; 
    private float minXAngle = -60f;
    private float rotateX = 0f;


    [Space, Header("타겟"),SerializeField]
    private Transform target;

    private Camera main_cam;
    private CameraManager cameraMgr;
    private InputManager inputMgr;
    private EventManager eventMgr;


    public GameObject onj;

    private float _w_speed = 8f;
    public float W_speed => _w_speed;

    private float _r_speed = 12f;
    public float R_speed => _r_speed;

    private float _s_speed = 5f;
    public float S_speed => _s_speed;


    private float _w_shake = 0.0005f;
    public float W_shake => _w_shake;

    private float _r_shake = 0.0008f;
    public float R_shake => _r_shake;

    private float _s_shake = 0.0003f;
    public float S_shake => _s_shake;



    void Start()
    {
        InGameCameraPos = new Vector3(target.position.x+ offSetX, transform.position.y, target.position.z + offSetZ);
        main_cam = gameObject.GetComponent<Camera>();
        inputMgr = GameManager.Instance.inputMgr;
        cameraMgr = GameManager.Instance.cameraMgr;
        eventMgr = GameManager.Instance.eventMgr;
    }

    
    void Update()
    {
        


        switch ((int)cameraMgr.ECameraState)
        {
            case 0:
                FollowCamera(main_cam, target, S_speed, S_shake, 1);
                RotateCamera();
                CheckWall();
                break;
            case 1:
                FollowCamera(main_cam, target, W_speed, W_shake, 1);
                RotateCamera();
                CheckWall();
                break;
            case 2:
                FollowCamera(main_cam, target, R_speed, R_shake, 1);
                RotateCamera();
                CheckWall();
                break;
            case 3:
                break;
            case 4:
                break;

            default:
                FollowCamera(main_cam, target, S_speed, S_shake, 1);
                RotateCamera();
                CheckWall();
                break;
        }




    }

    /// <summary>
    /// _version 은 0 :x 1: y 2:z
    /// </summary>
    /// <param 카메라="_cam"></param>
    /// <param 타겟="_target"></param>
    /// <param 속도="_speed"></param>
    /// <param amount="_shake"></param>
    /// <param xyz버전="_version"></param>
    public void FollowCamera(Camera _cam,Transform _target, float _speed, float _shake, int _version)
    {
        
        Vector3 v = new Vector3(_target.position.x+ offSetX, _cam.transform.position.y, _target.position.z + offSetZ);
        //v.y += 0.0002f / (2f * Mathf.PI / 4f) * Mathf.Sin(Time.time * 4f); 

        switch( _version )
        {
            case 0:
                v.x += _shake / (2f * Mathf.PI / _speed) * Mathf.Sin(Time.time * _speed);
                _cam.transform.position = v;
                break;
            case 1:
                v.y += _shake / (2f * Mathf.PI / _speed) * Mathf.Sin(Time.time * _speed);
                _cam.transform.position = v;
                break;
            case 2:
                v.z += _shake / (2f * Mathf.PI / _speed) * Mathf.Sin(Time.time * _speed);
                _cam.transform.position = v;
                break;

        }
        
    }

    public void RotateCamera() // 마우스 기반 X축 회전, 플레이어의 Y축 회전도 영향
    {
        
        rotateX = rotateX + inputMgr.CameraInputDir.x * sensitivity;
        rotateX = Mathf.Clamp(rotateX, minXAngle, maxXAngle); // 위, 아래 고정
        Quaternion playerRot = target.rotation;

        Quaternion quat = Quaternion.Euler(new Vector3(rotateX, 0, 0));
        transform.rotation = Quaternion.Lerp(transform.rotation, playerRot* quat, Time.deltaTime * rotSpeed);

    }


    public void CheckWall()
    {   
        
        Vector3 v = new Vector3(target.position.x, target.position.y+offSetY, target.position.z + offSetZ);
        int layerMask = (1 << 7); 
        layerMask = ~layerMask;

        if (Physics.Raycast(v,transform.forward, out RaycastHit _hit, 0.3f, layerMask))
        {
            transform.position = Vector3.Lerp(transform.position, _hit.point - transform.forward * cameraFix, cameraFixLerp * Time.deltaTime);

        }
        else
        {
            if (Vector3.Distance(transform.position, InGameCameraPos) > 0.2f)
            {
                transform.position = Vector3.Lerp(transform.position, InGameCameraPos, Time.deltaTime * cameraFixLerp);
            }
        }

    }



    public IEnumerator CameraShake(Camera _cam,float _shakeTime, float _shakeAmount)
    {
        float _timer = 0;
        float y = _cam.transform.position.y;
        while (_timer <= _shakeTime)
        {            
            _cam.transform.localPosition = _cam.transform.localPosition + Random.insideUnitSphere * _shakeAmount;            
            _timer += Time.deltaTime;
            yield return null;
        }
        _cam.transform.position = new Vector3(onj.transform.position.x, y, onj.transform.position.z);
    }

    public IEnumerator Shake(Camera _cam, float _shakeTime, float _shakeAmount)
    {
        float _timer = 0;
        float y = _cam.transform.position.y;
        Vector3 originalLocalPosition = _cam.transform.localPosition;
        _cam.transform.parent = target;
        while (_timer <= _shakeTime)
        {
            onj.transform.position = target.position;
            _cam.transform.localPosition = originalLocalPosition + Random.insideUnitSphere * _shakeAmount;

            _timer += Time.deltaTime;
            yield return null;
        }
        _cam.transform.position = new Vector3(transform.position.x, y, transform.position.z);
    }





    IEnumerator OnGravityFallCamera()
    {
        EventManager eventMgr = GameManager.Instance.eventMgr;
        
        yield return new WaitUntil(() => eventMgr.eCurEvent == ESOEventType.OnGravity);
        if(eventMgr.eCurEvent == ESOEventType.OnGravity&& eventMgr.eStageState == EstageEventState.Eventing)
        {
            StartCoroutine(CameraShake(main_cam, 2f, 0.8f));
            StartCoroutine(Move());
        }
    }

    public void ShakeCam()
    {
        StartCoroutine(OnGravityFallCamera());
    }

    IEnumerator Move()
    {
        while (true)
        {
            onj.transform.position = target.position;
            yield return null;
        }
    }
}
