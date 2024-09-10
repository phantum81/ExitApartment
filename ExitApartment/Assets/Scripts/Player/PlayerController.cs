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
    public Transform Player=>player;
    [Header("걷는속력"), SerializeField]
    private float _walkSpeed = 4f;
    public float WalkSpeed => _walkSpeed;
    [Header("뛰는속력"), SerializeField]
    private float _runSpeed = 8f;
    public float RunSpeed => _runSpeed;
    [Header("던지는 힘"), SerializeField]
    private float throwPower = 8f;

    public float limitSlope = 10f;


    private Rigidbody rigd;
    public Rigidbody Rigd => rigd;
    private float rotateY = 0f;
    //-----------이동관련---------------------------------

    [SerializeField]
    private PlayerSlopeRay playerSlopRay;
    [SerializeField]
    private GroundCheck groundCheck;
    public GroundCheck GrCheck=> groundCheck;
    [Header("단차체커"),SerializeField]
    private StepHeight stepHeight;




    private PlayerInventory playerInven;
    public PlayerInventory PlayerInven => playerInven;
    private InputManager inputMgr;
    private CameraManager cameraMgr;
    private UnitManager unitMgr;
    private SoundController soundCtr;
    public SoundController SoundCtr => soundCtr;
    [Header("플레이어 사운드"),SerializeField]
    private PlayerSound playerSound;
    public PlayerSound PSound => playerSound;

    private Vector3 origin;

    private EplayerState ePlayerState = EplayerState.None;
    private Coroutine curCoroutine;
    #region 유니티 실행부

    private void  Start()
    {
        Init();

    }

    private void Update()
    {
        InputDir = inputMgr.InputDir;
        if (playerSlopRay.IsSlope && groundCheck.IsGround)
            rigd.useGravity = false;
        else
            rigd.useGravity = true;
        

        if(playerSlopRay.GroundAngle > limitSlope)
            stepHeight.StepHeightMove(rigd);

    }
    #endregion


    #region 플레이어 기본 움직임

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

        inputDir.y = fallSpeed; // 떨어지는 속도 초기화
        rigd.velocity = inputDir;
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
            playerInven.AddList(_target);
            UiManager.Instance.inGameCtr.InvenCtr.AddItem(_data);
        }
        else if(UiManager.Instance.inGameCtr.InvenCtr.CheckInventoryFull())
        {
            return;
        }
        else
        {

            playerInven.AddList(_target);
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

    public IEnumerator ChangeSpeed(float _multiply, float _useTime)
    {
        
        if (curCoroutine == null)
        {
            float originWalkSpeed = _walkSpeed;
            float originRunSpeed = _runSpeed;
            _walkSpeed *= _multiply;
            _runSpeed *= _multiply;
            yield return new WaitForSeconds(_useTime);
            _walkSpeed = originWalkSpeed;
            _runSpeed = originRunSpeed;
            
            curCoroutine = null;

        }

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

    public void RotateModify(float _rot)
    {
        rotateY = _rot;
    }
}
