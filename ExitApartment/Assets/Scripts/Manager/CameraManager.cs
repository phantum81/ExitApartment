using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraManager : MonoBehaviour
{
    [Header("카메라 컨트롤러"),SerializeField]
    private CameraController cameraCtr;

    public CameraController CameraCtr=> cameraCtr;

    EcameraState eCameraState = EcameraState.FollowStand;
    public EcameraState ECameraState => eCameraState;  //카메라 스테이트 좀잘못쓴듯 캐릭터랑 겹침 구분할것

    EplayerState ePlayerState = EplayerState.Stand;
    
    void Start()
    {
        
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
                ePlayerState = EplayerState.Stand;
                break;
            case 1:
                eCameraState = EcameraState.FollowWalk;
                ePlayerState = EplayerState.Walk;
                break;
            case 2:
                eCameraState = EcameraState.FollowRun;
                ePlayerState = EplayerState.Run;
                break;
            default:
                eCameraState = EcameraState.FollowStand;
                ePlayerState = EplayerState.Stand;
                break;

        }
    }

}
