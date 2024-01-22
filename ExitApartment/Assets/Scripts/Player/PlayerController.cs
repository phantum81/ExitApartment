using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    [HideInInspector] public Vector3 InputDir { get; private set; }
    [Header("플레이어"),SerializeField]
    private Transform player;
    [Header("걷는속력"), SerializeField]
    private float _walkSpeed = 4f;
    public float WalkSpeed => _walkSpeed;
    [Header("뛰는속력"), SerializeField]
    private float _runSpeed = 8f;
    public float RunSpeed => _runSpeed;

    private Rigidbody rigd;
    private float rotateY = 0f;
    //-----------이동관련---------------------------------

    private Vector3 reserveGravity = new Vector3(0, 0, 1f);


    private InputManager inputMgr;
    private CameraManager cameraMgr;
    private EventManager eventMgr;
    private UnitManager unitMgr;

    [SerializeField]
    private UnityEvent onDead12F;

    private EplayerState ePlayerState = EplayerState.None;
    private EplayerMoveState eMoveState = EplayerMoveState.Stand;
    




    protected virtual void  Start()
    {
        inputMgr = GameManager.Instance.inputMgr;
        cameraMgr = GameManager.Instance.cameraMgr;
        eventMgr = GameManager.Instance.eventMgr;
        unitMgr= GameManager.Instance.unitMgr;

        reserveGravity.Normalize();
       

    }

    protected virtual void Update()
    {
       
        if (inputMgr.InputDir != Vector3.zero && eventMgr.eStageState == EstageEventState.None)
        {
            eMoveState = EplayerMoveState.Walk;
            if (inputMgr.IsShift)
                eMoveState = EplayerMoveState.Run;
        }
        else
            eMoveState = EplayerMoveState.Stand;



        switch (eMoveState)
        {
            case EplayerMoveState.Stand:
                
                break;
            case EplayerMoveState.Walk:
                Move(inputMgr.InputDir, WalkSpeed);
                
                break;
            case EplayerMoveState.Run:
                Move(inputMgr.InputDir, RunSpeed);
                
                break;
            case EplayerMoveState.Fall:
                
                break;

        }


        if(eventMgr.eStageState== EstageEventState.Die12F)
        {
            eventMgr.OnDead12F(onDead12F);
        }


        cameraMgr.ChangeCameraState((int)eMoveState);
        Rotate();



    }

    protected virtual void FixedUpdate()
    {
        if((int)eventMgr.eStageState == 1)
        {
            unitMgr.ChangeGravity(rigd,reserveGravity);

        }
    }


    public void Move(Vector3 _inputDir, float _speed)
    {
        Vector3 right= _inputDir.x*player.right;
        Vector3 foward = _inputDir.z*player.forward;
        Vector3 velocity = (right + foward).normalized;

        rigd.MovePosition(player.position + velocity * _speed * Time.deltaTime);

        

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


    







}
