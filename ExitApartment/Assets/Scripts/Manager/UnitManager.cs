using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    [Header("플레이어 컨트롤러"), SerializeField]
    private PlayerController playerCtr;
    public PlayerController PlayerCtr => playerCtr;
    [Header("엘리베이터 컨트롤러"), SerializeField]
    private ElevatorController elevatorCtr;
    public ElevatorController ElevatorCtr => elevatorCtr;

    [Header("에너미 컨트롤러"), SerializeField]
    private MonsterController mobCtr;
    public MonsterController MobCtr => mobCtr;



    [Header("충돌한 에너미 타겟"), SerializeField]
    private Transform contectTarget;
    public Transform ContectTarget => contectTarget;

    public List<GameObject> floorList;

    


    private Vector3 reserveGravity = new Vector3(0, 0, 1f);
    public Vector3 ReserveGravity => reserveGravity;

    /// <summary>
    /// 임시
    /// </summary>


    [Header("몹 리스트"), SerializeField]
    private List<GameObject> mobList = new List<GameObject>();
    [Header("쪽지 리스트"), SerializeField]
    private List<GameObject> PaperList = new List<GameObject>();

    private Dictionary<EMobType, GameObject> mobDic = new Dictionary<EMobType, GameObject>();
    public Dictionary<EMobType, GameObject> MobDic => mobDic;

    private Dictionary<ENoteType, GameObject> notePaperDic = new Dictionary<ENoteType, GameObject>();
    public Dictionary<ENoteType, GameObject> NotePaperDic => notePaperDic;


    private void Awake()
    {
        mobDic.Add(EMobType.Mob12F, mobList[0]);
        mobDic.Add(EMobType.Pumpkin, mobList[1]);
        notePaperDic.Add(ENoteType.Pumpkin, PaperList[0]);
    }
    void Start()
    {
        


        reserveGravity.Normalize();
        playerCtr.Init();
    }

    // Update is called once per frame
    void Update()
    {

    }


    private IEnumerator ChangeGravity(Rigidbody _rigd, Vector3 _gravity, float _startSpeed)
    {
        EventManager evMgr = GameManager.Instance.eventMgr;


        while (evMgr.eCurEvent == ESOEventType.OnGravity || evMgr.eCurEvent == ESOEventType.OnClear12F)
        {
            _rigd.useGravity = false;
            _startSpeed = Mathf.Clamp(_startSpeed, 0, 9.81f);
            _startSpeed += Time.deltaTime;
            _rigd.AddForce(_gravity * _startSpeed * _rigd.mass * 0.2f, ForceMode.Acceleration);
            yield return null;
        }
    }

    public void OnChangeGravity(Rigidbody _rigd, Vector3 _gravity, float _startSpeed)
    {
        StartCoroutine(ChangeGravity(_rigd, _gravity, _startSpeed));
    }




    public void GetContectTarget(Transform _target)
    {
        contectTarget = _target;
    }


    public void ShowObject(Transform _obj, bool _bool)
    {
        _obj.gameObject.SetActive(_bool);
    }





}
