using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

using Action = System.Action;
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
    public Action onLobbyFloor;

    public Action onHomeReset;
    public Action onForestReset;
    public Action onFallReset;
    public Action onNothingReset;
    public Action onEscapeReset;




    [Header("세팅데이터"),SerializeField]
    private SettingData settingData;
    public SettingData SetData => settingData;

    public GameData saveData;

    private int humanityScore = 0;
    public int HumanityScore => humanityScore;

    public EFloorType eFloorType = EFloorType.Home15EB;
    public EgameState eGameState = EgameState.Menu;

    [HideInInspector]
    public bool isClear12F = false;
    [HideInInspector]
    public bool isClearLocked =false;
    [HideInInspector]
    public bool isClearForest = false ;
    [HideInInspector]
    public bool isClearEscapeRoom = false;

    [HideInInspector]
    public bool isCheckCurboard = false;


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
        saveData.LoadData();
        
    }
    void Start()
    {
        if(SceneManager.GetActiveScene().name == "InGameScene")
        {
            eGameState = EgameState.InGame;
            eventMgr.SetIsPumpkinEvent(saveData.isPumpkinEvent);
            //ChangeFloorLevel(saveData.eFloorData);
        }
        else
        {
            eGameState = EgameState.Menu;
        }
       
        
    }

    
    void Update()
    {
        if(isRest)
        {
            if(saveData != null)
                ChangeFloorLevel(saveData.eFloorData);
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

        //if (inputMgr == null)
        //{
        //    GameObject _go = GameObject.Find("InputManager");
        //    if (_go != null)
        //    {
        //        inputMgr = _go.GetComponent<InputManager>();
        //    }
        //}

        itemMgr.Init();
        //inputMgr.Init();
        soundMgr.Init();
        unitMgr.Init();
        InitAction();
        isCheckCurboard = false;
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
                humanityScore = 0;
                break;
            case EFloorType.Nothing436A:
                onNothingFloor();
                humanityScore = 0;
                break;
            case EFloorType.Mob122F:
                onFallFloor();
                humanityScore = 0;
                break;
            case EFloorType.Forest5ABC:
                onForestFloor();
                humanityScore = 1;
                break;
            case EFloorType.Escape888B:
                onEscapeFloor();
                break;
            case EFloorType.Looby:
                onLobbyFloor();
                break;

        }
        eFloorType = _type;
        if(saveData != null)
            eventMgr.SetIsPumpkinEvent(saveData.isPumpkinEvent);
        Save(_type, eventMgr.GetIsPumpkinEvent());
       
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
    public void SetEscapeClearFloor(bool _clearFloor)
    {
        isClearEscapeRoom = _clearFloor;
    }


    public void SetGameState(EgameState _state)
    {
        eGameState = _state;
    }

    public void SetIsCheckCurboard(bool _isCheckCurboard)
    {
        isCheckCurboard = _isCheckCurboard;
    }

    public void Save(EFloorType _type, bool _bool)
    {
        if(saveData != null)
        {
            saveData.SaveData(_type, _bool);
        }
             
        
    }
    public void InitAction()
    {
        onGetForestHumanity = null;
        onForestFloor = null;
        onFallFloor = null;
        onNothingFloor = null;
        onEscapeFloor = null;
        onLobbyFloor = null; 

        onHomeReset = null;
        onForestReset = null;
        onFallReset = null;
        onNothingReset = null;
        onEscapeReset = null;

        isCheckCurboard= false;
        isClearLocked= false;
        isClearForest= false;
        isClear12F = false;
        isClearEscapeRoom= false;
        


    }
    public void Restart()
    {
        
        unitMgr.SkyBox.SetColor("_Tint", unitMgr.SkyboxOringinColor);
        SceneManager.LoadScene("InGameScene");
    }


    public IEnumerator CoTimer<T>(float _limitTime, T _param, Action<T> _callback )
    {
        
        yield return new WaitForSeconds( _limitTime);
        _callback.Invoke(_param);
    }
    public IEnumerator CoTimer(float _limitTime, Action _callback)
    {
        yield return new WaitForSeconds(_limitTime);
        _callback.Invoke();
    }

    private void SettingData()
    {

    }
}
