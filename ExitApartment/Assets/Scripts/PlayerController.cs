using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    [Header("플레이어"),SerializeField]
    private Transform player;
    [HideInInspector] public Vector3 InputDir { get; private set; }
    [Header("걷는속력"), SerializeField]
    private float _walkSpeed = 4f;
    public float WalkSpeed => _walkSpeed;
    [Header("뛰는속력"), SerializeField]
    private float _runSpeed = 8f;
    public float RunSpeed => _runSpeed;

    private Rigidbody rigd;
    private float rotateY = 0f;


    private Vector3 reserveGravity = new Vector3(0, 0, 1f);


    private InputManager inputMgr;
    private CameraManager cameraMgr;





    private EplayerState ePlayerState = EplayerState.Stand;
    private EstageEventState eStageState = EstageEventState.None;
    
    void Start()
    {
        inputMgr = GameManager.Instance.inputMgr;
        cameraMgr = GameManager.Instance.cameraMgr;
        reserveGravity.Normalize();
    }

    void Update()
    {

        //if (inputMgr.InputDir != Vector3.zero)
        //{
        //    if (inputMgr.IsShift)
        //    {
        //        Move(inputMgr.InputDir, RunSpeed, EplayerState.Run);

        //    }
        //    else
        //        Move(inputMgr.InputDir, WalkSpeed, EplayerState.Walk);
        //}
        //else if(ePlayerState== EplayerState.Fall)
        //    ePlayerState = EplayerState.Stand;



        if (inputMgr.InputDir != Vector3.zero)
        {
            ePlayerState = EplayerState.Walk;
            if (inputMgr.IsShift)
                ePlayerState = EplayerState.Run;
        }
        else if (ePlayerState != EplayerState.Fall)
            ePlayerState = EplayerState.Stand;




        switch (ePlayerState)
        {
            case EplayerState.Stand:
                break;
            case EplayerState.Walk:
                Move(inputMgr.InputDir, WalkSpeed, EplayerState.Walk);
                break;
            case EplayerState.Run:
                Move(inputMgr.InputDir, RunSpeed, EplayerState.Run);
                break;
            case EplayerState.Fall:
                break;

        }

        cameraMgr.ChangeCameraState((int)ePlayerState);
        Rotate();


    }

    private void FixedUpdate()
    {
        if((int)eStageState == 1)
        {
            ChangeGravity(rigd);
        }
    }


    public void Move(Vector3 _inputDir, float _speed, EplayerState _state)
    {
        Vector3 right= _inputDir.x*player.right;
        Vector3 foward = _inputDir.z*player.forward;
        Vector3 velocity = (right + foward).normalized;

        rigd.MovePosition(player.position + velocity * _speed * Time.deltaTime);

        ePlayerState = _state;

    }

    public void Init()
    {
        rigd= player.gameObject.GetComponent<Rigidbody>();        
    }
    public void Rotate() //마우스 움직임 기반 Y축 움직임
    {   
        rotateY = rotateY + inputMgr.CameraInputDir.y * cameraMgr.CameraCtr.Sensitivity;
        Quaternion quat = Quaternion.Euler(new Vector3(0, rotateY, 0));
        player.rotation = quat;
    }

    private void GravityRotate()
    {

    }



    public void ChangeStageState(int _state)
    {
        switch (_state)
        {
            case 0:
            eStageState = EstageEventState.None;
            break;
            case 1:
                eStageState = EstageEventState.GravityReverse;
                
                break;
            
        }
    }

    public void ChangeGravity(Rigidbody _rigd)
    {
        _rigd.useGravity= false;
        _rigd.AddForce(reserveGravity * 9.81f * _rigd.mass , ForceMode.Acceleration);
        
        
    }

}
