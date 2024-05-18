using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

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
    [Header("던지는 힘"), SerializeField]
    private float throwPower = 8f;



    private Rigidbody rigd;
    private float rotateY = 0f;
    //-----------이동관련---------------------------------


    
    private PlayerInventory playerInven;
    private InputManager inputMgr;
    private CameraManager cameraMgr;
    private UnitManager unitMgr;


    







    private EplayerState ePlayerState = EplayerState.None;

    #region 유니티 실행부

    private void  Start()
    {
        inputMgr = GameManager.Instance.inputMgr;
        cameraMgr = GameManager.Instance.cameraMgr;
        unitMgr = GameManager.Instance.unitMgr;
        playerInven = gameObject.GetComponent<PlayerInventory>();

    }

    private void Update()
    {
        InputDir = inputMgr.InputDir;        
    }
    #endregion


    #region 플레이어 기본 움직임

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

    public void Rotate() //마우스 움직임 기반 Y축 움직임
    {   
        rotateY = rotateY + inputMgr.CameraInputDir.y * cameraMgr.CameraCtr.Sensitivity;
        Quaternion quat = Quaternion.Euler(new Vector3(0, rotateY, 0));
        player.rotation = quat;
    }
    #endregion

    #region 플레이어 특정움직임


    public void PickItem(Transform _target, Vector3 _angle, ItemData _data)
    {
        if(UiManager.Instance.inGameCtr.InvenCtr.CheckInventoryEmpty() || playerInven.CurItem ==null)
        {
            playerInven.CurItem = _target;
            playerInven.InventoryItemList.Add(_target.gameObject);
            UiManager.Instance.inGameCtr.InvenCtr.AddItem(_data);
        }
        else if(UiManager.Instance.inGameCtr.InvenCtr.CheckInventoryFull())
        {
            return;
        }
        else
        {
            
            playerInven.InventoryItemList.Add(_target.gameObject);
            UiManager.Instance.inGameCtr.InvenCtr.AddItem(_data);
            _target.gameObject.SetActive(false);
        }

        AttachItem(_target, _angle);

    }

    private void AttachItem(Transform _target, Vector3 _angle)
    {
        _target.parent = playerInven.PickTransform;
        _target.transform.localPosition = new Vector3(0f, 0f, 0f);
        _target.localRotation = Quaternion.Euler(_angle);
        _target.GetComponent<Rigidbody>().useGravity = false;
        _target.GetComponent<Rigidbody>().isKinematic = true;
        _target.GetComponent<Collider>().enabled = false;
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
        playerInven.RemoveList(playerInven.CurItem);
        playerInven.CurItem = null;

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
