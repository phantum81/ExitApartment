using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject _go = GameObject.Find("GameManager");
                if (_go == null)
                {
                    _instance = _go.AddComponent<GameManager>();
                    
                }
                if (_instance == null)
                {
                    _instance = _go.GetComponent<GameManager>();
                }
            }
            return _instance;

        }
    }

    public UnitManager unitMgr;
    public CameraManager cameraMgr;
    public InputManager inputMgr;
    public EventManager eventMgr;
    public ItemManager itemMgr;
    public SoundManager soundMgr;
    public ScenesManager sceneMgr;

    public Action onGetForestHumanity;
    public Action onForestFloor;
    public Action onFallFloor;
    public Action onNothingFloor;
    public Action onEscapeFloor;

    public Action onHomeReset;
    public Action onForestReset;
    public Action onFallReset;
    public Action onNothingReset;
    public Action onEscapeReset;

    public SaveData saveData;


    private int humanityScore = 0;
    public int HumanityScore => humanityScore;

    public EFloorType eFloorType = EFloorType.Home15EB;
    [HideInInspector]
    public bool isClear12F = false;
    [HideInInspector]
    public bool isClearLocked =false;
    [HideInInspector]
    public bool isClearForest = false ;

    private bool isRest =false;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (_instance == null)
        {
            _instance = this;

        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        ChangeFloorLevel(saveData.data.eFloorData);

    }

    
    void Update()
    {
        if(isRest)
        {
            if(saveData != null)
                ChangeFloorLevel(saveData.data.eFloorData);
            isRest= false;
        }
    }


    public void Init()
    {
        if (unitMgr == null)
        {
            GameObject _go = GameObject.Find("UnitManager");
            if (_go != null)
            {
                unitMgr = _go.GetComponent<UnitManager>();
            }
        }

        if (cameraMgr == null)
        {
            GameObject _go = GameObject.Find("CameraManager");
            if (_go != null)
            {
                cameraMgr = _go.GetComponent<CameraManager>();
            }
        }

        if (eventMgr == null)
        {
            GameObject _go = GameObject.Find("EventManager");
            if (_go != null)
            {
                eventMgr = _go.GetComponent<EventManager>();
            }
        }

        if (itemMgr == null)
        {
            GameObject _go = GameObject.Find("ItemManager");
            if (_go != null)
            {
                itemMgr = _go.GetComponent<ItemManager>();
            }
        }

        if (soundMgr == null)
        {
            GameObject _go = GameObject.Find("SoundManager");
            if (_go != null)
            {
                soundMgr = _go.GetComponent<SoundManager>();
            }
        }

        if (inputMgr == null)
        {
            GameObject _go = GameObject.Find("InputManager");
            if (_go != null)
            {
                inputMgr = _go.GetComponent<InputManager>();
            }
        }
        if (saveData == null)
        {
            GameObject _go = GameObject.Find("SaveData");
            if (_go != null)
            {
                saveData = _go.GetComponent<SaveData>();
            }
        }
        itemMgr.Init();
        inputMgr.Init();
        soundMgr.Init();
        
        isRest = true;
        onGetForestHumanity += AddHumanityScore;
    }
    public bool CheckInterection(Ray _ray, out RaycastHit _hit, float _maxDis, int _layer)
    {
        return cameraMgr.CameraCtr.CheckInterection(_ray, out _hit ,_maxDis, _layer);
    }

    public void ZoomCamera( Camera _zoomCam, Camera _mainCam, Transform _target, float _distance)
    {
        cameraMgr.ZoomCamera.ZoomCamera( _zoomCam, _mainCam, _target, _distance);
    }

    public void ZoomMove(Camera _zoomCam, Collider _col, float _dis)
    {
        cameraMgr.ZoomCamera.StartCoroutine(cameraMgr.ZoomCamera.ZoomMove(_zoomCam, _col, _dis));
    }

    
    public void AddHumanityScore()
    {
        humanityScore++;
    }
    public int GetHumanityScore()
    {
        return humanityScore;
    }
    public void ChangeFloorLevel(EFloorType _type)
    {
        switch (_type)
        {
            case EFloorType.Home15EB:

                break;
            case EFloorType.Nothing436A:
                onNothingFloor();

                break;
            case EFloorType.Mob122F:
                onFallFloor();

                break;
            case EFloorType.Forest5ABC:
                onForestFloor();

                break;
            case EFloorType.Escape888B:
                onEscapeFloor();

                break;

        }
        eFloorType = _type;
        Save(_type);
       
    }

    public void Set12FClearFloor(bool _clearFloor)
    {
        isClear12F = _clearFloor;
    }
    public void SetForestClearFloor(bool _clearFloor)
    {
        isClearForest = _clearFloor;
    }
    public void SetLockedClearFloor(bool _clearFloor)
    {
        isClearLocked = _clearFloor;
    }

    public void RequestReset()
    {
        switch (unitMgr.ElevatorCtr.eCurFloor)
        {
            case EFloorType.Home15EB:
                onHomeReset();
                break;
            case EFloorType.Nothing436A:
                onNothingReset();
                break;
            case EFloorType.Mob122F:
                onFallReset();
                break;
            case EFloorType.Forest5ABC:
                onForestReset();
                break;
            case EFloorType.Escape888B:
                onEscapeReset();
                break;
        }
    }

    


    private void Save(EFloorType _type)
    {
        if(saveData != null)
             saveData.data.eFloorData = _type;
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
