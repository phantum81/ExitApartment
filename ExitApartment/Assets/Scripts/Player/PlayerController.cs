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
    [Header("�÷��̾�"),SerializeField]
    private Transform player;
    public Transform Player=>player;
    [Header("�ȴ¼ӷ�"), SerializeField]
    private float _walkSpeed = 4f;
    public float WalkSpeed => _walkSpeed;
    [Header("�ٴ¼ӷ�"), SerializeField]
    private float _runSpeed = 8f;
    public float RunSpeed => _runSpeed;
    [Header("������ ��"), SerializeField]
    private float throwPower = 8f;
    [Header("������ ����"), SerializeField]
    private float slopLimit = 30f;


    private Rigidbody rigd;
    public Rigidbody Rigd => rigd;
    private float rotateY = 0f;
    //-----------�̵�����---------------------------------

    [SerializeField]
    private PlayerSlopeRay playerSlopRay;
    [SerializeField]
    private GroundCheck groundCheck;
    public GroundCheck GrCheck=> groundCheck;
    private PlayerInventory playerInven;
    private InputManager inputMgr;
    private CameraManager cameraMgr;
    private UnitManager unitMgr;
    private SoundController soundCtr;
    public SoundController SoundCtr => soundCtr;
    [Header("�÷��̾� ����"),SerializeField]
    private PlayerSound playerSound;
    public PlayerSound PSound => playerSound;

    private Vector3 origin;

    private EplayerState ePlayerState = EplayerState.None;

    #region ����Ƽ �����

    private void  Start()
    {
        Init();

    }

    private void Update()
    {
        InputDir = inputMgr.InputDir;        
    }
    #endregion


    #region �÷��̾� �⺻ ������

    public void Move(Vector3 _inputDir, float _speed)
    {
        //Vector3 right= _inputDir.x * player.right;
        //Vector3 foward = _inputDir.z * player.forward;
        //Vector3 velocity = (right + foward).normalized;

        //rigd.AddForce(velocity * _speed * Time.deltaTime, ForceMode.VelocityChange);
        //Vector3 lastVelo = new Vector3(0f, rigd.velocity.y, 0f);
        //Vector3 lastAngular = new Vector3(0f, rigd.angularVelocity.y, 0f);
        //rigd.velocity = lastVelo;
        //rigd.angularVelocity = lastAngular;


        Vector3 right = _inputDir.x * player.right;
        Vector3 foward = _inputDir.z * player.forward;
        Vector3 inputDir = (right + foward).normalized;

        float fallSpeed = rigd.velocity.y;
        if (inputDir != Vector3.zero)
        {
            inputDir *= _speed;
        }
        else
            inputDir = Vector3.zero;

        inputDir.y = fallSpeed; // �������� �ӵ� �ʱ�ȭ
        rigd.velocity = inputDir;
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
        inputMgr = GameManager.Instance.inputMgr;
        cameraMgr = GameManager.Instance.cameraMgr;
        unitMgr = GameManager.Instance.unitMgr;
        soundCtr = GetComponent<SoundController>();
        playerInven = gameObject.GetComponent<PlayerInventory>();
        origin = player.position;
    }
    public void ChangeGravity()
    {
        unitMgr.OnChangeGravity(rigd,unitMgr.ReserveGravity,0f);
    }

    
}
