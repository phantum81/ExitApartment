using System.Collections;
using System.Collections.Generic;
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
    
    /// <summary>
    /// 0: 메인캠 1: 12f캠
    /// </summary>
    private Dictionary<int, Camera> camDic= new Dictionary<int, Camera>();
    /// <summary>
    /// 0: 메인캠 1: 12f캠
    /// </summary>
    public Dictionary<int, Camera> CameraDic => camDic;




    EcameraState eCameraState = EcameraState.FollowStand;
    public EcameraState ECameraState => eCameraState;

    EplayerMoveState eMovewState = EplayerMoveState.Stand;

    EstageEventState eStageState = EstageEventState.None;


    void Start()
    {
        camDic.Add(0, Camera.main);
        camDic.Add(1, deadCamCtr.GetComponent<Camera>());
        curCamera = camDic[0];
    }

    // Update is called once per frame
    void Update()
    {
        
        switch (GameManager.Instance.eventMgr.eStageState)
        {
            case EstageEventState.None:
                break;
            case EstageEventState.GravityReverse:
                break;
            case EstageEventState.Die12F:                
                StartCoroutine(cameraCtr.CameraShake(camDic[0], 0.5f, 0.1f));
                StartCoroutine(ChangeCamera(camDic[1], 0.5f));
                break;
            case EstageEventState.Eventing:
                
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
                eCameraState = EcameraState.FollowStand;
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
        curCamera.gameObject.SetActive(false);
        _cam.gameObject.SetActive(true);

        curCamera = _cam;

    }

}
