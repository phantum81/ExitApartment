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
    private ItemManager itemMgr;
    private SoundController soundCtr;
    public SoundController SoundCtr => soundCtr;
    [Header("플레이어 사운드"),SerializeField]
    private PlayerSound playerSound;
    public PlayerSound PSound => playerSound;

    private Vector3 origin;
    private float groundAngle;
    private EplayerState ePlayerState = EplayerState.None;
    private Coroutine curCoroutine;
    #region 유니티 실행부

    private void  Start()
    {
        Init();

    }

    private void Update()
    {
        InputDir = InputLocalize(inputMgr.InputDir);
        groundAngle = playerSlopRay.CalculateGroundAngle(InputDir);


        if (groundAngle>0 && groundCheck.IsGround)
        {
            rigd.useGravity = false;
            rigd.velocity = new Vector3(rigd.velocity.x, 0f, rigd.velocity.z);
        }
        else
            rigd.useGravity = true;


        stepHeight.StepHeightMove(rigd, InputDir);

       

    }
    #endregion


    #region 플레이어 기본 움직임

    public void Move(Vector3 _inputDir, float _speed)
    {


        float fallSpeed = rigd.velocity.y;
        if (_inputDir != Vector3.zero)
        {
            _inputDir *= _speed;
        }
        else
            _inputDir = Vector3.zero;

        _inputDir.y = fallSpeed; // 떨어지는 속도 초기화
        rigd.velocity = _inputDir;
        Rotate();
        

    }

    public void Rotate() //마우스 움직임 기반 Y축 움직임
    {   
        rotateY = rotateY + inputMgr.CameraInputDir.y * cameraMgr.CameraCtr.Sensitivity;
        Quaternion quat = Quaternion.Euler(new Vector3(0, rotateY, 0));
        player.rotation = quat;
    }
    private Vector3 InputLocalize(Vector3 _inputdir)
    {
        Vector3 right = _inputdir.x * player.right;
        Vector3 foward = _inputdir.z * player.forward;
        Vector3 inputDir = (right + foward).normalized;
        return inputDir;
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
        itemMgr.SetLayerRecursively(_target.gameObject, 12);
        _target.GetComponent<Rigidbody>().useGravity = false;
        _target.GetComponent<Rigidbody>().isKinematic = true;
        _target.GetComponent<Collider>().enabled = false;
        unitMgr.SetShadowCast(_target.gameObject, false);
    }
    

    public void ThrowItem(Transform _target, float _time)
    {
        Vector3 _dir = player.forward + player.up;
        Rigidbody rd = _target.GetComponent<Rigidbody>();
        _target.GetComponent<Collider>().enabled = true;
        _target.parent = null;
        itemMgr.SetLayerRecursively(_target.gameObject, 6);
        rd.useGravity = true;
        rd.isKinematic = false;
        if(_time < 1f)
        {
            _time = 1f;
        }
        rd.AddForce(_dir.normalized * 40f * _time * throwPower * Time.fixedDeltaTime);
        playerInven.RemoveList(playerInven.CurItem);
        playerInven.CurItem = null;
        unitMgr.SetShadowCast(_target.gameObject, true);

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
        itemMgr = GameManager.Instance.itemMgr;
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
