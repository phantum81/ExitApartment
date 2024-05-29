using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UIElements;

public class CameraManager : MonoBehaviour
{
    [Header("카메라 컨트롤러"),SerializeField]
    private CameraController cameraCtr;

    public CameraController CameraCtr=> cameraCtr;
    [Header("데드카메라 컨트롤러12F"), SerializeField]
    private OnDeadCameraController deadCamCtr;




    [Header("현재카메라"),SerializeField]
    private Camera curCamera;
    public Camera CurCamera => curCamera;

    [Header("Ui카메라"), SerializeField]
    private Camera uiCamera;
    [Header("줌 카메라"), SerializeField]
    private ZoomCameraController zoomCamera;
    public ZoomCameraController ZoomCamera => zoomCamera;





    private Dictionary<int, Camera> camDic= new Dictionary<int, Camera>();
    /// <summary>
    /// 0: 메인캠 1: 12f캠 2: 줌 캠 3: ui 캠
    /// </summary>
    public Dictionary<int, Camera> CameraDic => camDic;


    [SerializeField]
    EcameraState eCameraState = EcameraState.FollowStand;
    public EcameraState ECameraState => eCameraState;

    [SerializeField]
    EplayerMoveState eMovewState = EplayerMoveState.Stand;

    EstageEventState eStageState = EstageEventState.None;

    private void Awake()
    {
        camDic.Add(0, Camera.main);
        camDic.Add(1, deadCamCtr.GetComponent<Camera>());
        camDic.Add(2, zoomCamera.GetComponent<Camera>());
        camDic.Add(3, uiCamera);

        curCamera = camDic[0];
        foreach (Camera cam in camDic.Values)
        {
            cam.enabled = false;
        }
        curCamera.enabled = true;
        camDic[3].enabled = true;
        
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
        switch (GameManager.Instance.eventMgr.eCurEvent)
        {
            case ESOEventType.OnGravity:
                break;
            case ESOEventType.OnDie12F:
                ChangeCamera(camDic[1]);
                break;
        }


    }


    public void ChangeCameraState(int _state)
    {
        switch (_state)
        {
            case 0:
                eCameraState = EcameraState.FollowStand;
                eMovewState = EplayerMoveState.Stand;
                break;
            case 1:
                eCameraState = EcameraState.FollowWalk;
                eMovewState = EplayerMoveState.Walk;
                break;
            case 2:
                eCameraState = EcameraState.FollowRun;
                eMovewState = EplayerMoveState.Run;
                break;

            case 3:
                eMovewState = EplayerMoveState.Fall;
                eCameraState = EcameraState.FollowFall;
                break;
            case 4:
                eMovewState = EplayerMoveState.None;
                eCameraState = EcameraState.None;
                break;

            default:
                eCameraState = EcameraState.FollowStand;
                eMovewState = EplayerMoveState.Stand;
                break;

        }
    }

    public void ChangeCamera(Camera _cam, float _time = 0f)
    {        
        curCamera.enabled=false;
        _cam.enabled=true;

        curCamera = _cam;
        
    }

    
    public bool CheckObjectInCamera(GameObject _target)
    {
        if (_target.transform.parent.gameObject.activeSelf)
        {
            Vector3 screenPoint = CurCamera.WorldToViewportPoint(_target.transform.position);
            bool isIn = screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
            return isIn;
        }
        return false;



    }


}
