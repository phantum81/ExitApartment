using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraManager : MonoBehaviour
{
    [Header("카메라 컨트롤러"),SerializeField]
    private CameraController cameraCtr;

    public CameraController CameraCtr=> cameraCtr;

    EcameraState ePlayerState = EcameraState.FollowStand;
    public EcameraState EPlayerState => ePlayerState;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


    }
    public void ChangeState(int _state)
    {
        switch (_state)
        {
            case 0:
                ePlayerState= EcameraState.FollowStand;
                break;
            case 1:
                ePlayerState= EcameraState.FollowWalk;
                break;
            case 2:
                ePlayerState= EcameraState.FollowRun;
                break;
            default:
                ePlayerState = EcameraState.FollowStand;
                break;

        }
    }

}
