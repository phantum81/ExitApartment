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

    


    private InputManager inputMgr;
    private CameraManager cameraMgr;
    private UnitManager unitMgr;

    private PlayerPostProcess playerProcess;
    private Transform curItem;
    [SerializeField]
    private Transform pickTransform;
    private EplayerState ePlayerState = EplayerState.None;

   

    private  void  Start()
    {
        inputMgr = GameManager.Instance.inputMgr;
        cameraMgr = GameManager.Instance.cameraMgr;
        unitMgr = GameManager.Instance.unitMgr;

        playerProcess = gameObject.GetComponent<PlayerPostProcess>();
        
       

    }

    private void Update()
    {
        InputDir = inputMgr.InputDir;

        playerProcess.ChangePlayerState((int)ePlayerState);


    }



    public void Move(Vector3 _inputDir, float _speed)
    {
        Vector3 right= _inputDir.x * player.right;
        Vector3 foward = _inputDir.z * player.forward;
        Vector3 velocity = (right + foward).normalized;

        rigd.MovePosition(player.position + velocity * _speed * Time.fixedDeltaTime);

        Rotate();

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




    public void ChangeGravity()
    {
        unitMgr.OnChangeGravity(rigd,unitMgr.ReserveGravity,0f);
    }

    public void PickItem(Transform _target)
    {
        curItem = _target;
        _target.parent = player;
        _target.transform.position = pickTransform.position;
        _target.GetComponent<Rigidbody>().useGravity = false;
        _target.GetComponent<Collider>().enabled = false;
    }
    public void ThrowItem(Transform _target, float _power, Vector3 _dir)
    {
        Rigidbody rd = _target.GetComponent<Rigidbody>();
        curItem = null;
        _target.parent = null;        
        rd.useGravity = true;
        rd.AddForce(_dir.normalized * _power * Time.fixedDeltaTime);
        _target.GetComponent<Collider>().enabled = true;
    }
}
