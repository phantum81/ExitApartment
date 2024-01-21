using System.Collections;
using System.Collections.Generic;
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
    }

    
    void Update()
    {
        CheckWall();
        switch ((int)cameraMgr.ECameraState)
        {
            case 0:
                FollowCamera(S_speed, S_shake);
                break;
            case 1:
                FollowCamera(W_speed, W_shake);
                break;
            case 2:
                FollowCamera(R_speed, R_shake);
                break;
            default:
                FollowCamera(S_speed, S_shake);
                break;
        }

    }

    public void FollowCamera(float _speed, float _shake)
    {
        
        Vector3 v = new Vector3(target.position.x+ offSetX, transform.position.y, target.position.z + offSetZ);
        //v.y += 0.0002f / (2f * Mathf.PI / 4f) * Mathf.Sin(Time.time * 4f);        
        v.y += _shake / (2f * Mathf.PI / _speed) * Mathf.Sin(Time.time * _speed);
        transform.position = v;
        RotateCamera();
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



}
