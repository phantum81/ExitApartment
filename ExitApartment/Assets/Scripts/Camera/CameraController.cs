using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.UI.Image;

public class CameraController : MonoBehaviour
{
    
    private Vector3 InGameCameraPos;

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
    
    private float minXAngle = -90f;
    private float rotateX = 0f;


    [Space, Header("타겟"),SerializeField]
    private Transform target;
    [Header("타겟 머리 위치"), SerializeField]
    private Transform target_head;


    [Header("멘탈공격 몹"), SerializeField]
    private GameObject mentalMob;
    


    private Camera main_cam;
    private CameraManager cameraMgr;
    private InputManager inputMgr;





    Vector3 shakeDir = new Vector3(30f,60f,40f);

    [Header("걷는 카메라 흔들림 속도"),SerializeField]
    private float _w_speed = 9f;
    public float W_speed => _w_speed;

    [Header("달리는 카메라 흔들림 속도"),SerializeField]
    private float _r_speed = 14f;
    public float R_speed => _r_speed;

    [Header("서있는 카메라 흔들림 속도"),SerializeField]
    private float _s_speed = 5f;
    public float S_speed => _s_speed;

    [Header("걷는 흔들림 정도"),SerializeField]
    private float _w_shake = 0.0007f;
    public float W_shake => _w_shake;

    [Header("달리는 흔들림 정도"),SerializeField]
    private float _r_shake = 0.0009f;
    public float R_shake => _r_shake;

    [Header("서있는 흔들림 정도"),SerializeField]
    private float _s_shake = 0.00006f;
    public float S_shake => _s_shake;
    private Vector3 _velocity = Vector3.zero;
    private float originCamY;
    private float groundHeight;
    #region 유니티 실행부
    void Start()
    {
        InGameCameraPos = new Vector3(target.position.x, transform.position.y, target.position.z);
        main_cam = gameObject.GetComponent<Camera>();
        inputMgr = GameManager.Instance.inputMgr;
        cameraMgr = GameManager.Instance.cameraMgr;
        originCamY = transform.position.y - target.transform.position.y;
        
    }

    
    void Update()
    {

        sensitivity = GetSensitivity();


        switch ((int)cameraMgr.ECameraState)
        {
            case 0:
                FollowCamera(main_cam, target, S_speed, S_shake, 1);
                RotateCamera();
                //CheckWall();
                break;
            case 1:
                FollowCamera(main_cam, target, W_speed, W_shake, 1);
                RotateCamera();
                //CheckWall();
                break;
            case 2:
                FollowCamera(main_cam, target, R_speed, R_shake, 1);
                RotateCamera();
                //CheckWall();
                break;
            case 3:
                break;
            case 4:
                break;

            default:
                FollowCamera(main_cam, target, S_speed, S_shake, 1);
                RotateCamera();
                //CheckWall();
                break;
        }

        


    }
    #endregion



    #region 기본 카메라 기능
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
        
        Vector3 v = new Vector3(_target.position.x, _cam.transform.position.y, _target.position.z);
        Vector3 y = target_head.position;
        //v.y += 0.0002f / (2f * Mathf.PI / 4f) * Mathf.Sin(Time.time * 4f); 

        switch( _version )
        {
            case 0:
                v.x += _shake / (2f * Mathf.PI / _speed) * Mathf.Sin(Time.time * _speed);
                _cam.transform.position = v;
                break;
            case 1:
                y.y += _shake / (2f * Mathf.PI / _speed) * Mathf.Sin(Time.time * _speed);
                target_head.transform.position = y;
                Vector3 newCameraPosition = new Vector3(v.x, target_head.position.y, v.z);

                _cam.transform.position = Vector3.SmoothDamp(_cam.transform.position, newCameraPosition, ref _velocity, 0.1f);
                //_cam.transform.position = new Vector3(v.x, target_head.position.y, v.z);
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
        
        Vector3 v = new Vector3(target.position.x, target.position.y, target.position.z);
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

    }//문제가있다. 고쳐야함 (떨림, 이동고정문제)

    public void ResetCamYPosition()
    {
        target_head.position = new Vector3(target_head.position.x, originCamY+target.transform.position.y , target_head.position.z);
        
    }


    #endregion

    #region 카메라 흔들림

    public IEnumerator CameraShake(Camera _cam, float _shakeTime, float _shakeAmount)
    {

        _cam.transform.parent = cameraMgr.ShakeObj;
        float _timer = 0;
        Vector3 originalPosition = _cam.transform.localPosition;
        Vector3 shakePos = _cam.transform.parent.localPosition;
        while (_timer <= _shakeTime)
        {
            _cam.transform.parent.position = shakePos + Random.insideUnitSphere * _shakeAmount;
            _timer += Time.deltaTime;
            yield return null;
        }

        _cam.transform.localPosition = originalPosition;
        _cam.transform.parent = null;

    }
    public IEnumerator CameraShakeOrigin(Camera _cam, float _shakeTime, float _shakeAmount)
    {
        Debug.Log("CameraShakeOrigin 코루틴 시작");
        float _timer = 0;
        Vector3 originalPosition = _cam.transform.localPosition;

        while (_timer <= _shakeTime)
        {
            Debug.Log($"카메라 위치: {_cam.transform.localPosition}");
            _cam.transform.localPosition = originalPosition + Random.insideUnitSphere * _shakeAmount;
            _timer += Time.deltaTime;
            yield return null;
        }

        _cam.transform.localPosition = originalPosition;
        Debug.Log("CameraShakeOrigin 코루틴 종료");
    }

    public IEnumerator ShakeRotateCam(Camera _cam, float _shakeTime, float _shakeAmount, Vector3 _shakeDir, float _shakingSpeed)
    {
        float _timer = 0f;
        Quaternion originRotate = _cam.transform.localRotation;
        float shake = 0f;
        while (_timer <= _shakeTime)
        {
            float rotX = Random.Range(-_shakeDir.x, _shakeDir.x);
            float rotY = Random.Range(-_shakeDir.y, _shakeDir.y);
            float rotZ = Random.Range(-_shakeDir.z, _shakeDir.z);

            Vector3 rotationV = originRotate.eulerAngles + new Vector3(rotX, rotY, rotZ);
            Quaternion rotationQ = Quaternion.Euler(rotationV);
            while (Quaternion.Angle(_cam.transform.localRotation, rotationQ) > 0.1f)
            {
                shake += Time.deltaTime * _shakingSpeed;
                Mathf.Clamp(shake, 5f, _shakeAmount);
                _cam.transform.localRotation = Quaternion.RotateTowards(_cam.transform.localRotation, rotationQ, shake * Time.deltaTime);
                _timer += Time.deltaTime;
                yield return null;
            }

            yield return null;

        }
    }

    #endregion

    

    #region 이벤트 코루틴

    private IEnumerator OnGravityFallCamera(Camera _cam, float _shakeTime, float _shakeAmount, Vector3 _shakeDir, float _shakingSpeed)
    {
        _cam.transform.parent = target;

        yield return StartCoroutine(ShakeRotateCam(_cam, _shakeTime, _shakeAmount, _shakeDir, _shakingSpeed));

        yield return new WaitForSeconds(0.5f);

        _cam.transform.parent = null;

    }

    IEnumerator OnCameraShake()
    {
        yield return new WaitForSeconds(1f);
        StartCoroutine(CameraShake(main_cam, 1.3f, 0.3f));

    }
    #endregion

    #region 유니티 이벤트 등록부

    public void OnGravityCam()
    {        
        StartCoroutine(OnGravityFallCamera(main_cam, 2.5f, 100f, shakeDir, 80f));
    }

    public void OnClear12FCamera()
    {
        StartCoroutine(OnCameraShake());
    }
    #endregion

    #region  그외 기능
    public bool CheckInterection(Ray _ray , out RaycastHit _hit, float _maxDis, int _layer )
    {
        
        if(Physics.Raycast(_ray, out _hit, _maxDis, _layer))
        {
            return true;
        }
        else
            return false;
    }

    public float GetSensitivity()
    {
        return GameManager.Instance.SetData.Sensitivity;
    }

    #endregion
}
