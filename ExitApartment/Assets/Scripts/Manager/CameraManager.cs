using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraManager : MonoBehaviour
{
    [Header("카메라 컨트롤러"),SerializeField]
    private CameraController cameraCtr;

    public CameraController CameraCtr=> cameraCtr;

    

    private Dictionary<int, Camera> camDic= new Dictionary<int, Camera>();


    EcameraState eCameraState = EcameraState.FollowStand;
    public EcameraState ECameraState => eCameraState;

    EplayerMoveState eMovewState = EplayerMoveState.Stand;

    EstageEventState eStageState = EstageEventState.None;


    void Start()
    {
        camDic.Add(0, Camera.main);
        
    }

    // Update is called once per frame
    void Update()
    {
        

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
            default:
                eCameraState = EcameraState.FollowStand;
                eMovewState = EplayerMoveState.Stand;
                break;

        }
    }

    

}
