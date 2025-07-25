using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class InGameUiShower : MonoBehaviour, IInGameMenuView
{
    [Header("상호작용 마크"),SerializeField]
    private GameObject pickMark;
    [Header("센터 마크"), SerializeField]
    private GameObject centerMark;


    [Header("상호작용 텍스트"), SerializeField]
    private TextMeshProUGUI interactionTxt;

    [Header("죽음 판넬"), SerializeField]
    private GameObject diePanel;

    [Header("시작검은 화면 판넬"), SerializeField]
    private GameObject startPanel;

    [Header("메뉴 판넬"), SerializeField]
    private GameObject menuPanel;

    [Header("옵션 판넬"), SerializeField]
    private GameObject optionPanel;

    [Header("종료확인 판넬"), SerializeField]
    private GameObject questionPanel;

    [Header("인벤토리 창"), SerializeField]
    private GameObject inventoryPan;

    [Header("층 입력 텍스트"), SerializeField]
    private TextMeshPro writeFloor_txt;

    [Header("현재 층 텍스트"), SerializeField]
    private TextMeshPro curFloor_txt;

    [Header("인간성 리스트"), SerializeField]
    private List<GameObject> UiHumanityList = new List<GameObject>();

    [Header("인간성 텍스트"), SerializeField]
    private TextMeshProUGUI humanityText;


    [Header("엘리베이터 오류 문자"), SerializeField]
    private TextMeshProUGUI elevatorError_txt;


    [Header("옵션 버튼"), SerializeField]
    private Button optionBtn;
    [Header("메인화면 버튼"), SerializeField]
    private Button mainMenuBtn;
    [Header("종료 버튼"), SerializeField]
    private Button escBtn;
    [Header("예 버튼"), SerializeField]
    private Button yesBtn;
    [Header("아니오 버튼"), SerializeField]
    private Button noBtn;



    private InputManager inputMgr;
    string interactionAction;
    private PlayerInteraction playerInteraction;
    private EplayerState ePlayerState;
    private EInteractionType hitType;
    private ElevatorController elevatorCtr;
    private UiManager uiMgr;
    private MenuPresent menuPresent;
    private CameraManager cameraMgr;
    private LanguageManager languageMgr;
    void Start()
    {
        menuPresent = new MenuPresent(this);
        elevatorCtr = GameManager.Instance.unitMgr.ElevatorCtr;
        playerInteraction = GameManager.Instance.unitMgr.PlayerCtr.gameObject.GetComponent<PlayerInteraction>();
        inputMgr = GameManager.Instance.inputMgr;
        uiMgr = UiManager.Instance;
        //GameManager.Instance.onGetForestHumanity += ActiveHumanity;
        StartCoroutine(ResetScreen());
        StartCoroutine(uiMgr.SetUiInvisible(startPanel.transform, 1f, 0.5f));
        optionBtn.onClick.AddListener(menuPresent.ShowOptionPanel);
        escBtn.onClick.AddListener(menuPresent.CloseGameClick);
        mainMenuBtn.onClick.AddListener(menuPresent.LoadMenuClick);
        cameraMgr = GameManager.Instance.cameraMgr;
        languageMgr = GameManager.Instance.languageMgr;
    }

    // Update is called once per frame
    void Update()
    {
        ePlayerState = HFSM<EplayerState, PlayerPostProcess>.Instance.CurState;

        if (inputMgr.InputDic[EuserAction.Ui_Menu])
        {
            ActiveUi(!menuPanel.activeSelf, menuPanel);
            if (!menuPanel.activeSelf)
            {
                ActiveUi(false, optionPanel);
                ActiveUi(false, questionPanel);
                ActiveUi(false, optionPanel);
            }
            if (menuPanel.activeSelf)
            {
                GameManager.Instance.SetGameState(EgameState.Menu);
                Time.timeScale = 0f;
                AudioListener.pause = true; 
            }
            else
            {
                GameManager.Instance.SetGameState(EgameState.InGame);
                Time.timeScale = 1f;
                AudioListener.pause = false;
            }
        }

        if (inputMgr.InputDic[EuserAction.Inventory])
        {            
            ActiveUi(!inventoryPan.activeSelf, inventoryPan);
            if (inventoryPan.activeSelf)
            {
                GameManager.Instance.SetGameState(EgameState.Menu);
            }
            else
            {
                GameManager.Instance.SetGameState(EgameState.InGame);
            }
            
        }

        if (cameraMgr.CurCamera == cameraMgr.CameraDic[0] || cameraMgr.CurCamera == cameraMgr.CameraDic[2])
        {
            if (playerInteraction.CheckInteraction())
            {
                ActiveUi(true, pickMark);

                if (playerInteraction.PreHit)
                {
                    hitType = playerInteraction.PreHit.GetComponent<IInteraction>().OnGetType();

                    interactionAction = GetTextIngameTable(hitType,languageMgr.LocalizedCache);

                    ReWrite(interactionTxt, $"{interactionAction}({inputMgr.Inputbind.BindingDic[EuserAction.Interaction]})");
                }
            }
            else
            {
                ActiveUi(false, pickMark);

            }
        }
        else
        {
            ActiveUi(false, pickMark);
            ActiveUi(false, centerMark);
        }



        if (humanityText.gameObject.activeSelf)
        {
            ReWrite(humanityText, $"{GameManager.Instance.HumanityScore}");
        }


    }


    private void ActiveHumanity()
    {
        for(int i=0; i< UiHumanityList.Count; i++)
        {
            ActiveUi(true, UiHumanityList[i].gameObject);
            StartCoroutine(uiMgr.SetUiInvisible(UiHumanityList[i].transform, 2f, 2f));
        }
       
    }


    public void ActiveUi(bool _bool, GameObject _ui)
    {
        _ui.SetActive(_bool);

    }

    public void RenewWriteFloor(string _txt)
    {
        if (writeFloor_txt.GetTextInfo(writeFloor_txt.text).characterCount < 4)
            writeFloor_txt.text += _txt;
        else
            writeFloor_txt.text = string.Empty;
    }
    public void ClearText(TextMeshPro _text)
    {
        _text.text = string.Empty;
    }
    public void RenewCurFloor(string _txt)
    {
        curFloor_txt.text = _txt;
    }

    public string GetCurFloor()
    {
        return curFloor_txt.text;
    }


    public bool CheckRightFloor()
    {
        if (!elevatorCtr.IsClose)
        {
            ErrorMessage(GetTextIngameTable(EErrorType.NotClose, languageMgr.ErrorLocalizedCache));
            return false;
        }
        if (curFloor_txt.text == writeFloor_txt.text)
        {
            ErrorMessage(GetTextIngameTable(EErrorType.Same, languageMgr.ErrorLocalizedCache));
            return false;
        }
        else if (writeFloor_txt.text == string.Empty)
        {
            ErrorMessage(GetTextIngameTable(EErrorType.NotPress, languageMgr.ErrorLocalizedCache));
            return false;
        }        
        else
        {
            switch(GameManager.Instance.eFloorType)
            {
                case EFloorType.Home15EB:
                    if (writeFloor_txt.text == UnitManager.LOCKED_FLOOR)
                    {
                        SetElevatorFloorText(UnitManager.LOCKED_FLOOR);
                        return true;
                    }
                    else if(writeFloor_txt.text == UnitManager.HOME_FLOOR)
                    {
                        SetElevatorFloorText(UnitManager.HOME_FLOOR);
                        return true;
                    }
                    if (!elevatorError_txt.gameObject.activeSelf)
                    {
                        ErrorMessage(GetTextIngameTable(EErrorType.NotGo, languageMgr.ErrorLocalizedCache));
                    }

                    return false;
                case EFloorType.Nothing436A:
                    if(writeFloor_txt.text == UnitManager.HOME_FLOOR)
                    {
                        SetElevatorFloorText(UnitManager.HOME_FLOOR);
                        return true;
                    }
                    else if(writeFloor_txt.text == UnitManager.Fall_FLOOR)
                    {
                        SetElevatorFloorText(UnitManager.Fall_FLOOR);
                        return true;
                    }
                    if (!elevatorError_txt.gameObject.activeSelf)
                    {
                        ErrorMessage(GetTextIngameTable(EErrorType.NotGo, languageMgr.ErrorLocalizedCache));
                    }
                    return false;
                case EFloorType.Mob122F:

                    if (writeFloor_txt.text == UnitManager.HOME_FLOOR)
                    {
                        SetElevatorFloorText(UnitManager.HOME_FLOOR);
                        return true;
                    }
                    else if (writeFloor_txt.text == UnitManager.FOREST_FLOOR)
                    {
                        SetElevatorFloorText(UnitManager.FOREST_FLOOR);
                        return true;
                    }
                    if (!elevatorError_txt.gameObject.activeSelf)
                    {
                        ErrorMessage(GetTextIngameTable(EErrorType.NotGo, languageMgr.ErrorLocalizedCache));
                    }
                    return false;


                case EFloorType.Forest5ABC:
                    if(writeFloor_txt.text == UnitManager.HOME_FLOOR)
                    {
                        SetElevatorFloorText(UnitManager.HOME_FLOOR);                        
                        return true;
                    }
                    else if(writeFloor_txt.text == UnitManager.ESCAPE_FLOOR)
                    {
                        SetElevatorFloorText(UnitManager.ESCAPE_FLOOR);
                        return true;
                    }
                    if (!elevatorError_txt.gameObject.activeSelf)
                    {
                        ErrorMessage(GetTextIngameTable(EErrorType.NotGo, languageMgr.ErrorLocalizedCache));
                    }
                    return false;
                case EFloorType.Escape888B:
                    if (writeFloor_txt.text == UnitManager.LOBBY_FLOOR)
                    {
                        SetElevatorFloorText(UnitManager.LOBBY_FLOOR);
                        return true;
                    }
                    if (!elevatorError_txt.gameObject.activeSelf)
                    {
                        ErrorMessage(GetTextIngameTable(EErrorType.NotGo, languageMgr.ErrorLocalizedCache));
                    }
                    return false;

                case EFloorType.Looby:
                    if (!elevatorError_txt.gameObject.activeSelf)
                    {
                        ErrorMessage(GetTextIngameTable(EErrorType.NotWork, languageMgr.ErrorLocalizedCache));
                    }

                    return false;


                default:
                    if (!elevatorError_txt.gameObject.activeSelf)
                    {
                        ErrorMessage(GetTextIngameTable(EErrorType.NotGo, languageMgr.ErrorLocalizedCache));
                    }
                    return false;
            }
        }
        
        
    }

    public void ErrorMessage(string _value)
    {
        ActiveUi(true, elevatorError_txt.gameObject);
        ReWrite(elevatorError_txt, _value);
        StartCoroutine(uiMgr.SetUiInvisible(elevatorError_txt.transform, 2f, 2f));
    }
    private void SetElevatorFloorText(string _value)
    {
        RenewCurFloor(_value);
        ClearText(writeFloor_txt);
    }
    private void ReWrite(TextMeshProUGUI _text, string _main)
    {
        _text.text = _main;
    }
    public string GetTextIngameTable<T>(T _hitType, Dictionary<T, string> _cache) where T : Enum
    {

        if(_cache.TryGetValue(_hitType, out string localizedActionString))
            return localizedActionString;


        return localizedActionString;
        

    }


    private IEnumerator ResetScreen()
    {
        while (true)
        {
            yield return new WaitUntil(() => ePlayerState == EplayerState.Die);
            ActiveUi(true, diePanel);
            yield return StartCoroutine(uiMgr.SetUiVisible(diePanel.transform, 1f, 1f));
            GameManager.Instance.Restart();
        }
    }
    public void ScreenChange(float _time =1f, float _wait = 1f)
    {
        ActiveUi(true, startPanel);
        StartCoroutine(uiMgr.SetUiInvisible(startPanel.transform, _time, _wait));
    }
    public void ScreenVisibleChange(float _time = 1f, float _wait = 1f)
    {
        ActiveUi(true, diePanel);
        StartCoroutine(uiMgr.SetUiVisible(diePanel.transform, _time, _wait));
    }
    public void LoadMenuScene()
    {
        
        SceneManager.LoadScene("MenuScene");
    }

    public void CloseGame()
    {
        //EditorApplication.isPlaying = false;
        Application.Quit();
    }

    public void CloseGameClick()
    {
        ActiveUi(true, questionPanel.gameObject);
        yesBtn.onClick.RemoveAllListeners();
        noBtn.onClick.RemoveAllListeners();
        noBtn.onClick.AddListener(() => { ActiveUi(false, questionPanel.gameObject); });
        yesBtn.onClick.AddListener(() => CloseGame());
    }
    public void LoadMenuClick()
    {
        ActiveUi(true, questionPanel.gameObject);
        yesBtn.onClick.RemoveAllListeners();
        noBtn.onClick.RemoveAllListeners();
        noBtn.onClick.AddListener(() => { ActiveUi(false, questionPanel.gameObject); });
        yesBtn.onClick.AddListener(() => LoadMenuScene());

    }

    public void ShowOptionPanel()
    {
        ActiveUi(true, optionPanel.gameObject);
    }

}
