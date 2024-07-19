using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.UIElements;

public class CameraManager : MonoBehaviour
{
    [Header("ī�޶� ��Ʈ�ѷ�"),SerializeField]
    private CameraController cameraCtr;

    public CameraController CameraCtr=> cameraCtr;
    [Header("����ī�޶� ��Ʈ�ѷ�12F"), SerializeField]
    private OnDeadCameraController deadCamCtr;


    [Header("��鸲 ���� ������Ʈ"), SerializeField]
    private Transform shakeObj;
    public Transform ShakeObj => shakeObj;

    [Header("����ī�޶�"),SerializeField]
    private Camera curCamera;
    public Camera CurCamera => curCamera;

    [Header("Uiī�޶�"), SerializeField]
    private Camera uiCamera;
    [Header("�� ī�޶�"), SerializeField]
    private ZoomCameraController zoomCamera;
    public ZoomCameraController ZoomCamera => zoomCamera;

    [Header("Ÿ�Ӷ��� ����"),SerializeField]
    private List<TimelineAsset> timelineAsset;
    public List<TimelineAsset> TimelineAssets=> timelineAsset;
    [Header("����Ʈ���μ���"),SerializeField]
    private PlayerPostProcess postProcess;
    public PlayerPostProcess PostProcess => postProcess;
    private Dictionary<int, Camera> camDic= new Dictionary<int, Camera>();
    /// <summary>
    /// 0: ����ķ 1: 12fķ 2: �� ķ 3: ui ķ
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
        if(curCamera == _cam) return;
        //postProcess.StartCoroutine(postProcess.CloseCameraVignette());
        curCamera.enabled=false;
        _cam.enabled=true;

        curCamera = _cam;
        
    }

    
    public bool CheckObjectInCamera(GameObject _target)
    {

        if (_target.transform.parent.gameObject.activeSelf && GameManager.Instance.unitMgr.ElevatorCtr.eCurFloor == EFloorType.Home15EB)
        {
            Vector3 screenPoint = CurCamera.WorldToViewportPoint(_target.transform.position);
            bool isIn = screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;

            if (isIn)
            {
                // ī�޶󿡼� ��� ������Ʈ������ ���� ���� ���
                Vector3 directionToTarget = _target.transform.position - CurCamera.transform.position;

                // ����ĳ��Ʈ�� ����Ͽ� ��ֹ��� �ִ��� Ȯ��
                if (Physics.Raycast(CurCamera.transform.position, directionToTarget, out RaycastHit hit, 20f ,1<<9))
                {
                    // ����ĳ��Ʈ�� ��� ������Ʈ�� ��Ҵٸ� true, �׷��� ������ false
                    return false;
                }
                else 
                    return true;
            }
        }
        return false;


    }

    public void ChangeTimeLineAsset(PlayableDirector _direct, TimelineAsset _asset)
    {
        _direct.Stop();
        _direct.playableAsset = _asset;
    }

    


}
