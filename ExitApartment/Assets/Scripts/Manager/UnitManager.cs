using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;
using Color = UnityEngine.Color;
public class UnitManager : MonoBehaviour
{
    public const string HOME_FLOOR = "15EB";
    public const string Fall_FLOOR = "122F";
    public const string LOCKED_FLOOR = "436A";
    public const string FOREST_FLOOR = "5ABC";
    public const string ESCAPE_FLOOR = "888B";
    public const string REASON_TEXT = "Fuck Off";
    public const string NAME_TEXT = "kys";
    public const string ADDRESS_TEXT = "15EB1";


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


    private Terrain terrain;
    private TerrainData terrainData;

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



    [Header("�� ��ȣ �ؽ�Ʈ"), SerializeField]
    private List<Transform> floorNumTextList = new List<Transform>();

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

    private Dictionary<ESeePoint, Transform> seePointsDic = new Dictionary<ESeePoint, Transform>();
    public Dictionary<ESeePoint, Transform> SeePointsDic => seePointsDic;

    private void Awake()
    {
        GameManager.Instance.unitMgr = this;
        mobDic.Add(EMobType.Mob12F, mobList[0]);
        mobDic.Add(EMobType.Pumpkin, mobList[1]);
        mobDic.Add(EMobType.SkinLess, mobList[2]);
        mobDic.Add(EMobType.Crab, mobList[3]);
        mobDic.Add(EMobType.Bat, mobList[4]);
        notePaperDic.Add(ENoteType.Pumpkin, PaperList[0]);
        notePaperDic.Add(ENoteType.Forest, PaperList[1]);
        notePaperDic.Add(ENoteType.Mob12F, PaperList[2]);
        notePaperDic.Add(ENoteType.Last, PaperList[3]);

        skyboxOringinColor = skyBox.GetColor("_Tint");
    }
    void Start()
    {

        GameManager.Instance.onGetForestHumanity += OnForestHumanity;
        GameManager.Instance.onNothingFloor += ShowClearNothingFloor;
        GameManager.Instance.onFallFloor += ShowClearFallFloor;
        GameManager.Instance.onForestFloor += ShowClearForestFloor;
        
        reserveGravity.Normalize();
        playerCtr.Init();
        terrain = Terrain.activeTerrain;
        terrainData = terrain?.terrainData;
        ChangeFloor(EFloorType.Home15EB);
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




    public void SetContectTarget(Transform _target)
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
    public void ChangeFloor(EFloorType _type)
    {
        for(int i =0; i<floorList.Count; i++)
        {
            if (i == (int)_type)
            {
                floorList[i].SetActive(true);
            }
            else
                floorList[i].SetActive(false);
        }
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
        
    }

    public void ChangeMaterial(Transform _target, Material _mat)
    {
        _target.GetComponent<Renderer>().material = _mat;
    }


    public int GetTerrainTextureIndex(Vector3 _position, TerrainData _data, Terrain _terrain)
    {
        Vector3 terrainLocalPos = _position - _terrain.transform.position;
        int mapX = Mathf.RoundToInt((terrainLocalPos.x / _data.size.x) * _data.alphamapWidth);
        int mapZ = Mathf.RoundToInt((terrainLocalPos.z / _data.size.z) * _data.alphamapHeight);

        float[,,] alphaMap = _data.GetAlphamaps(mapX, mapZ, 1, 1);
        int textureIndex = 0;
        float maxMix = 0.0f;

        for (int i = 0; i < alphaMap.GetLength(2); i++)
        {
            if (alphaMap[0, 0, i] > maxMix)
            {
                textureIndex = i;
                maxMix = alphaMap[0, 0, i];
            }
        }

        return textureIndex;
    }

    public bool CheckTerrainDetail(Vector3 _position, TerrainData _data, Terrain _terrain, int range, params int[] _excludeLayerIndex)
    {
        Vector3 terrainLocalPos = _position - _terrain.transform.position;
        int detailX = Mathf.RoundToInt((terrainLocalPos.x / _data.size.x) * _data.detailWidth);
        int detailZ = Mathf.RoundToInt((terrainLocalPos.z / _data.size.z) * _data.detailHeight);

        // ���� ���� ��ǥ�� ���
        int minX = Mathf.Clamp(detailX - range, 0, _data.detailWidth - 1);
        int maxX = Mathf.Clamp(detailX + range, 0, _data.detailWidth - 1);
        int minZ = Mathf.Clamp(detailZ - range, 0, _data.detailHeight - 1);
        int maxZ = Mathf.Clamp(detailZ + range, 0, _data.detailHeight - 1);

        // ��� ���̾ �˻�
        for (int layer = 0; layer < _data.detailPrototypes.Length; layer++)
        {
            // �����Ϸ��� ���̾�� �ǳʶ�
            if (System.Array.Exists(_excludeLayerIndex, element => element == layer))
            {
                continue;
            }

            int width = maxX - minX + 1;
            int height = maxZ - minZ + 1;

            int[,] detailLayer = _data.GetDetailLayer(minX, minZ, width, height, layer);

            // ���� ���� ������ ��ü�� �ִ��� Ȯ��
            for (int x = 0; x < width; x++)
            {
                for (int z = 0; z < height; z++)
                {
                    if (detailLayer[x, z] > 0)
                    {
                        return true;
                    }
                }
            }
        }

        // ���ܵ� ���̾ ������ ��� ���̾ ������ ��ü�� ���� ���
        return false;
    }

    private void ShowClearNothingFloor()
    {
        floorNumTextList[0].gameObject.SetActive(true);
    }
    private void ShowClearFallFloor()
    {

        floorNumTextList[1].gameObject.SetActive(true);
        notePaperDic[ENoteType.Mob12F].SetActive(true);
        ChangeMaterial(apartInfoPaper.transform, apartPaperMatList[1]);
        

    }
    private void ShowClearForestFloor()
    {
        ShowObject(notePaperDic[ENoteType.Forest].transform, true);
        ShowObject(notePaperDic[ENoteType.Last].transform, true);
        ChangeMaterial(apartInfoPaper.transform, apartPaperMatList[2]);
        StartCoroutine(ChangeMaterialColor(SkyBox, Color.red, SkyboxOringinColor, 4f));
    }

    public IEnumerator LerpValue(Action<float> _value, float _min, float _max, float _inverseSpeed, float _lerpRatio)
    {
        yield return null;
        float elapsedTime = 0f;
        while (true)
        {
            _value(Mathf.Lerp(_min, _max, elapsedTime / (_inverseSpeed * _lerpRatio)));


            if (elapsedTime >= _inverseSpeed)
            {
                break;
            }

            elapsedTime += Time.deltaTime;

            yield return null;
        }
    }
    public IEnumerator RandomLerpValue(Action<float> _value, float _min, float _max, float _inverseSpeed, float _lerpRatio)
    {
        yield return null;

        float elapsedTime = 0f;

        while (true)
        {
            // �� �����Ӹ��� ���� �� ����
            float randomMin = Random.Range(_min, _max);
            float randomMax = Random.Range(_min, _max);

            // ������ ���� ���� �׻� randomMin�� �� �۵��� ����
            if (randomMin > randomMax)
            {
                float temp = randomMin;
                randomMin = randomMax;
                randomMax = temp;
            }

            elapsedTime = 0f; // elapsedTime�� �ʱ�ȭ

            while (elapsedTime < _inverseSpeed)
            {
                float lerpValue = Mathf.Lerp(randomMin, randomMax, elapsedTime / (_inverseSpeed * _lerpRatio));
                _value(lerpValue);

                elapsedTime += Time.deltaTime;

                yield return null; // ���� �����ӱ��� ���
            }

            // ������ �� ����
            _value(randomMax);
        }
    }



    void OnApplicationQuit()
    {
        skyBox.SetColor("_Tint", skyboxOringinColor);
    }
 
    public void Init()
    {
        skyBox.SetColor("_Tint", skyboxOringinColor);
        foreach(var mob in notePaperDic.Values)
        {
            mob.gameObject.SetActive(false);
        }
        foreach(var floorNum in floorNumTextList)
        {
            floorNum.gameObject.SetActive(false);
        }

    }
}
