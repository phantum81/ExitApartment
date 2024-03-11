using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UIElements;

public class CameraManager : MonoBehaviour
{
    [Header("Ä«¸Þ¶ó ÄÁÆ®·Ñ·¯"),SerializeField]
    private CameraController cameraCtr;

    public CameraController CameraCtr=> cameraCtr;
    [Header("µ¥µåÄ«¸Þ¶ó ÄÁÆ®·Ñ·¯12F"), SerializeField]
    private OnDeadCameraController deadCamCtr;




    [Header("ÇöÀçÄ«¸Þ¶ó"),SerializeField]
    private Camera curCamera;
    public Camera CurCamera => curCamera;

    [Header("UiÄ«¸Þ¶ó"), SerializeField]
    private Camera uiCamera;
    [Header("ÁÜ Ä«¸Þ¶ó"), SerializeField]
    private ZoomCameraController zoomCamera;
    public ZoomCameraController ZoomCamera => zoomCamera;


    /// <summary>
    /// 0: ¸ÞÀÎÄ· 1: 12fÄ·
    /// </summary>
    private Dictionary<int, Camera> camDic= new Dictionary<int, Camera>();
    /// <summary>
    /// 0: ¸ÞÀÎÄ· 1: 12fÄ· 2: ÁÜ Ä· 3: ui Ä·
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
                StartCoroutine(ChangeCamera(camDic[1]));
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

    public IEnumerator ChangeCamera(Camera _cam, float _time = 0f)
    {        
        curCamera.enabled=false;
        _cam.enabled=true;

        curCamera = _cam;
        yield return null;
    }




}
