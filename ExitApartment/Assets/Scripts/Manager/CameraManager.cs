using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UIElements;

public class CameraManager : MonoBehaviour
{
    [Header("ī�޶� ��Ʈ�ѷ�"),SerializeField]
    private CameraController cameraCtr;

    public CameraController CameraCtr=> cameraCtr;
    [Header("����ī�޶� ��Ʈ�ѷ�12F"), SerializeField]
    private OnDeadCameraController deadCamCtr;

    [Header("����ī�޶�"),SerializeField]
    private Camera curCamera;

    [Header("Uiī�޶�"), SerializeField]
    private Camera UiCamera;


    /// <summary>
    /// 0: ����ķ 1: 12fķ
    /// </summary>
    private Dictionary<int, Camera> camDic= new Dictionary<int, Camera>();
    /// <summary>
    /// 0: ����ķ 1: 12fķ
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
        camDic.Add(2, UiCamera);
        curCamera = camDic[0];
        camDic[1].enabled = false;
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
                StartCoroutine(ChangeCamera(camDic[1], 0f));
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

    IEnumerator ChangeCamera(Camera _cam, float _time)
    {

        yield return new WaitForSeconds(_time);
        curCamera.enabled=false;
        _cam.enabled=true;

        curCamera = _cam;

    }




}
