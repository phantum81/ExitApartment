using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using Color = UnityEngine.Color;
public class UnitManager : MonoBehaviour
{
    [Header("�÷��̾� ��Ʈ�ѷ�"), SerializeField]
    private PlayerController playerCtr;
    public PlayerController PlayerCtr => playerCtr;
    [Header("���������� ��Ʈ�ѷ�"), SerializeField]
    private ElevatorController elevatorCtr;
    public ElevatorController ElevatorCtr => elevatorCtr;

    [Header("���ʹ� ��Ʈ�ѷ�"), SerializeField]
    private MonsterController mobCtr;
    public MonsterController MobCtr => mobCtr;



    [Header("�浹�� ���ʹ� Ÿ��"), SerializeField]
    private Transform contectTarget;
    public Transform ContectTarget => contectTarget;

    


    private Vector3 reserveGravity = new Vector3(0, 0, 1f);
    public Vector3 ReserveGravity => reserveGravity;

    //--------------��ųʸ��� �����Ұ�
    /// <summary>
    /// �ӽ�
    /// </summary>

    public List<GameObject> floorList;


    [Header("�� ����Ʈ"), SerializeField]
    private List<GameObject> mobList = new List<GameObject>();
    [Header("���� ����Ʈ"), SerializeField]
    private List<GameObject> PaperList = new List<GameObject>();

    [Header("����Ʈ ������"), SerializeField]
    private GameObject apartInfoPaper;

    [Header("����Ʈ ������ ���׸���"), SerializeField]
    private List<Material> apartPaperMatList;

    [Header("��ī�̹ڽ�"), SerializeField]
    private Material skyBox;
    public Material SkyBox=> skyBox;


    //-----------��ųʸ��� �����Ұ�.




    private UnityEngine.Color skyboxOringinColor;
    public UnityEngine.Color SkyboxOringinColor => skyboxOringinColor;

    private Dictionary<EMobType, GameObject> mobDic = new Dictionary<EMobType, GameObject>();
    public Dictionary<EMobType, GameObject> MobDic => mobDic;

    private Dictionary<ENoteType, GameObject> notePaperDic = new Dictionary<ENoteType, GameObject>();
    public Dictionary<ENoteType, GameObject> NotePaperDic => notePaperDic;


    private void Awake()
    {
        mobDic.Add(EMobType.Mob12F, mobList[0]);
        mobDic.Add(EMobType.Pumpkin, mobList[1]);
        mobDic.Add(EMobType.SkinLess, mobList[2]);
        mobDic.Add(EMobType.Crab, mobList[3]);
        notePaperDic.Add(ENoteType.Pumpkin, PaperList[0]);
        notePaperDic.Add(ENoteType.Forest, PaperList[1]);
        notePaperDic.Add(ENoteType.Mob12F, PaperList[2]);
        notePaperDic.Add(ENoteType.Last, PaperList[3]);
        skyboxOringinColor = skyBox.GetColor("_Tint");
    }
    void Start()
    {
        GameManager.Instance.onGetForestHumanity += OnForestHumanity;
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


    public IEnumerator CorChangeLightColor(Light _light, Color _start, Color _end, float _time)
    {
        float curTime = 0f;
        while (curTime < _time)
        {
            curTime += Time.deltaTime;
            _light.color = Color.Lerp(_start, _end, curTime / _time);
            yield return null;
        }
        _light.color = _end;
    }


    public IEnumerator ChangeMaterialColor(Material _mat, Color _start, Color _end, float _time )
    {
        float curTime = 0f;
        while (curTime < _time)
        {
            curTime += Time.deltaTime;
            Color lerpColor = Color.Lerp(_start, _end, curTime / _time);
            _mat.SetColor("_Tint", lerpColor);
            yield return null;
        }
        _mat.SetColor("_Tint", _end);
    }

    public void OnForestHumanity()
    {
        ShowObject(notePaperDic[ENoteType.Forest].transform, true);
        ChangeMaterial(apartInfoPaper.transform, apartPaperMatList[1]);
    }

    public void ChangeMaterial(Transform _target, Material _mat)
    {
        _target.GetComponent<Renderer>().material = _mat;
    }


    void OnApplicationQuit()
    {
        skyBox.SetColor("_Tint", skyboxOringinColor);
    }
}
