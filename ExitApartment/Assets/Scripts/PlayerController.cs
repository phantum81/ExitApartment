using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    [Header("�÷��̾�"),SerializeField]
    private Transform player;
    [HideInInspector] public Vector3 InputDir { get; private set; }
    [Header("�ȴ¼ӷ�"), SerializeField]
    private float _walkSpeed = 4f;
    public float WalkSpeed => _walkSpeed;
    [Header("�ٴ¼ӷ�"), SerializeField]
    private float _runSpeed = 8f;
    public float RunSpeed => _runSpeed;

    private Rigidbody rigd;


    private float rotateY = 0f;

    private InputManager inputMgr;
    private CameraManager cameraMgr;
    private EplayerState ePlayerState = EplayerState.Stand;
    
    void Start()
    {
        inputMgr = GameManager.Instance.inputMgr;
        cameraMgr = GameManager.Instance.cameraMgr;
    }

    void Update()
    {

        if (inputMgr.InputDir != Vector3.zero)
        {
            if (inputMgr.IsShift)
            {
                Move(inputMgr.InputDir, RunSpeed, EplayerState.Run);

            }
            else
                Move(inputMgr.InputDir, WalkSpeed, EplayerState.Walk);
        }
        else
            ePlayerState = EplayerState.Stand;
        cameraMgr.ChangeState((int)ePlayerState);
        Rotate();


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
    public void Rotate() //���콺 ������ ��� Y�� ������
    {   
        rotateY = rotateY + inputMgr.CameraInputDir.y * cameraMgr.CameraCtr.Sensitivity;
        Quaternion quat = Quaternion.Euler(new Vector3(0, rotateY, 0));
        player.rotation = quat;
    }

}
