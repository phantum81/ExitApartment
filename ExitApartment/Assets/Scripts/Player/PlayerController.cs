using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    [HideInInspector] public Vector3 InputDir { get; private set; }
    [Header("�÷��̾�"),SerializeField]
    private Transform player;
    [Header("�ȴ¼ӷ�"), SerializeField]
    private float _walkSpeed = 4f;
    public float WalkSpeed => _walkSpeed;
    [Header("�ٴ¼ӷ�"), SerializeField]
    private float _runSpeed = 8f;
    public float RunSpeed => _runSpeed;
    [Header("������ ��"), SerializeField]
    private float throwPower = 8f;



    private Rigidbody rigd;
    private float rotateY = 0f;
    //-----------�̵�����---------------------------------

    


    private InputManager inputMgr;
    private CameraManager cameraMgr;
    private UnitManager unitMgr;

    

    private Transform curItem;
    public Transform CurItem => curItem;



    [Header("�ֿ� ������ ��ġ"),SerializeField]
    private Transform pickTransform;

    private EplayerState ePlayerState = EplayerState.None;

    #region ����Ƽ �����

    private void  Start()
    {
        inputMgr = GameManager.Instance.inputMgr;
        cameraMgr = GameManager.Instance.cameraMgr;
        unitMgr = GameManager.Instance.unitMgr;

    }

    private void Update()
    {
        InputDir = inputMgr.InputDir;        
    }
    #endregion


    #region �÷��̾� �⺻ ������

    public void Move(Vector3 _inputDir, float _speed)
    {
        Vector3 right= _inputDir.x * player.right;
        Vector3 foward = _inputDir.z * player.forward;
        Vector3 velocity = (right + foward).normalized;
        
        rigd.AddForce(velocity * _speed * Time.deltaTime, ForceMode.VelocityChange);
        rigd.velocity = Vector3.zero;
        rigd.angularVelocity = Vector3.zero;

        Rotate();

    }

    public void Rotate() //���콺 ������ ��� Y�� ������
    {   
        rotateY = rotateY + inputMgr.CameraInputDir.y * cameraMgr.CameraCtr.Sensitivity;
        Quaternion quat = Quaternion.Euler(new Vector3(0, rotateY, 0));
        player.rotation = quat;
    }
    #endregion

    #region �÷��̾� Ư��������


    public void PickItem(Transform _target, Vector3 _angle)
    {
        if(curItem == null)
        {
            
            curItem = _target;
            _target.parent = pickTransform;
            _target.transform.localPosition = new Vector3(0f,0f,0f);
            _target.localRotation = Quaternion.Euler(_angle);
            _target.GetComponent<Rigidbody>().useGravity = false;
            _target.GetComponent<Rigidbody>().isKinematic = true;
            _target.GetComponent<Collider>().enabled = false;

            
            
        }
        else
        {
            ThrowItem(curItem);
            PickItem(_target,_angle);
        }

    }
    public void ThrowItem(Transform _target)
    {
        Vector3 _dir = transform.forward;
        Rigidbody rd = _target.GetComponent<Rigidbody>();
        _target.GetComponent<Collider>().enabled = true;
        _target.parent = null;        
        rd.useGravity = true;
        rd.isKinematic = false;
        rd.AddForce(_dir.normalized * 40f * throwPower * Time.fixedDeltaTime);
        curItem = null;
    }


    #endregion


    public void Init()
    {
        rigd = player.gameObject.GetComponent<Rigidbody>();
    }
    public void ChangeGravity()
    {
        unitMgr.OnChangeGravity(rigd,unitMgr.ReserveGravity,0f);
    }
}
