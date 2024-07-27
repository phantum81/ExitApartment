using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class InGameUiShower : MonoBehaviour
{
    [Header("상호작용 마크"),SerializeField]
    private GameObject pickMark;
    [Header("상호작용 텍스트"), SerializeField]
    private TextMeshProUGUI interactionTxt;

    [Header("죽음 판넬"), SerializeField]
    private GameObject diePanel;

    [Header("시작검은 화면 판넬"), SerializeField]
    private GameObject startPanel;


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




    private InputManager inputMgr;
    string interactionAction;
    private PlayerInteraction playerInteraction;
    private EplayerState ePlayerState;
    private EInteractionType hitType;
    private ElevatorController elevatorCtr;
    private UiManager uiMgr;


    void Start()
    {
        elevatorCtr = GameManager.Instance.unitMgr.ElevatorCtr;
        playerInteraction = GameManager.Instance.unitMgr.PlayerCtr.gameObject.GetComponent<PlayerInteraction>();
        inputMgr = GameManager.Instance.inputMgr;
        uiMgr = UiManager.Instance;
        GameManager.Instance.onGetForestHumanity += ActiveHumanity;
        StartCoroutine(ResetScreen());
        StartCoroutine(uiMgr.SetUiInvisible(startPanel.transform, 1f, 0.5f));
    }

    // Update is called once per frame
    void Update()
    {
        ePlayerState = HFSM<EplayerState, PlayerPostProcess>.Instance.CurState;



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

     

        if (playerInteraction.CheckInteraction())
        {
            ActiveUi(true, pickMark);

            if (playerInteraction.PreHit)
            {
                hitType = playerInteraction.PreHit.GetComponent<IInteraction>().OnGetType();
                
                interactionAction = GetInteractionAction(hitType);

                ReWrite(interactionTxt, $"{interactionAction}({inputMgr.Inputbind.BindingDic[EuserAction.Interaction]})");
            }
            
        }
        else
        {
            ActiveUi(false, pickMark);
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
        if (curFloor_txt.text == writeFloor_txt.text || !elevatorCtr.IsClose)
        {
            ActiveUi(true, elevatorError_txt.gameObject);
            ReWrite(elevatorError_txt, "같은 층 이다..");
            StartCoroutine(uiMgr.SetUiInvisible(elevatorError_txt.transform, 2f, 2f));
            return false;
        }
        else if (writeFloor_txt.text == string.Empty)
        {
            ActiveUi(true, elevatorError_txt.gameObject);
            ReWrite(elevatorError_txt, "층을 입력해라..");
            StartCoroutine(uiMgr.SetUiInvisible(elevatorError_txt.transform, 2f, 2f));
            return false;
        }
        else
        {
            switch(GameManager.Instance.eFloorType)
            {
                case EFloorType.Home15EB:
                    if (writeFloor_txt.text == UnitManager.LOCKED_FLOOR)
                    {
                        RenewCurFloor(UnitManager.LOCKED_FLOOR);
                        ClearText(writeFloor_txt);
                        return true;
                    }
                    else if(writeFloor_txt.text == UnitManager.HOME_FLOOR)
                    {
                        RenewCurFloor(UnitManager.HOME_FLOOR);
                        ClearText(writeFloor_txt);
                        return true;
                    }
                    if (!elevatorError_txt.gameObject.activeSelf)
                    {
                        ActiveUi(true, elevatorError_txt.gameObject);
                        ReWrite(elevatorError_txt, "갈 수 없는 듯 하다...");
                        StartCoroutine(uiMgr.SetUiInvisible(elevatorError_txt.transform, 2f, 2f));
                    }

                    return false;
                case EFloorType.Nothing436A:
                    if(writeFloor_txt.text == UnitManager.HOME_FLOOR)
                    {
                        RenewCurFloor(UnitManager.HOME_FLOOR);
                        ClearText(writeFloor_txt);
                        return true;
                    }
                    else if(writeFloor_txt.text == UnitManager.Fall_FLOOR)
                    {
                        RenewCurFloor(UnitManager.Fall_FLOOR);
                        ClearText(writeFloor_txt);
                        return true;
                    }
                    if (!elevatorError_txt.gameObject.activeSelf)
                    {
                        ActiveUi(true, elevatorError_txt.gameObject);
                        ReWrite(elevatorError_txt, "갈 수 없는 듯 하다...");
                        StartCoroutine(uiMgr.SetUiInvisible(elevatorError_txt.transform, 2f, 2f));
                    }
                    return false;
                case EFloorType.Mob122F:

                    if (writeFloor_txt.text == UnitManager.HOME_FLOOR)
                    {
                        RenewCurFloor(UnitManager.HOME_FLOOR);
                        ClearText(writeFloor_txt);
                        return true;
                    }
                    else if (writeFloor_txt.text == UnitManager.FOREST_FLOOR)
                    {
                        RenewCurFloor(UnitManager.FOREST_FLOOR);
                        ClearText(writeFloor_txt);
                        return true;
                    }
                    if (!elevatorError_txt.gameObject.activeSelf)
                    {
                        ActiveUi(true, elevatorError_txt.gameObject);
                        ReWrite(elevatorError_txt, "갈 수 없는 듯 하다...");
                        StartCoroutine(uiMgr.SetUiInvisible(elevatorError_txt.transform, 2f, 2f));
                    }
                    return false;


                case EFloorType.Forest5ABC:
                    if(writeFloor_txt.text == UnitManager.HOME_FLOOR)
                    {
                        RenewCurFloor(UnitManager.HOME_FLOOR);
                        ClearText(writeFloor_txt);
                        return true;
                    }
                    else if(writeFloor_txt.text == UnitManager.ESCAPE_FLOOR)
                    {
                        RenewCurFloor(UnitManager.ESCAPE_FLOOR);
                        ClearText(writeFloor_txt);
                        return true;
                    }
                    if (!elevatorError_txt.gameObject.activeSelf)
                    {
                        ActiveUi(true, elevatorError_txt.gameObject);
                        ReWrite(elevatorError_txt, "갈 수 없는 듯 하다...");
                        StartCoroutine(uiMgr.SetUiInvisible(elevatorError_txt.transform, 2f, 2f));
                    }
                    return false;
                case EFloorType.Escape888B:
                    if (!elevatorError_txt.gameObject.activeSelf)
                    {
                        ActiveUi(true, elevatorError_txt.gameObject);
                        ReWrite(elevatorError_txt, "갈 수 없는 듯 하다...");
                        StartCoroutine(uiMgr.SetUiInvisible(elevatorError_txt.transform, 2f, 2f));
                    }
                    return false;

                 default:
                    if (!elevatorError_txt.gameObject.activeSelf)
                    {
                        ActiveUi(true, elevatorError_txt.gameObject);
                        ReWrite(elevatorError_txt, "갈 수 없는 듯 하다...");
                        StartCoroutine(uiMgr.SetUiInvisible(elevatorError_txt.transform, 2f, 2f));
                    }
                    return false;
            }
        }
        
        
    }


    private void ReWrite(TextMeshProUGUI _text, string _main)
    {
        _text.text = _main;
    }
    string GetInteractionAction(EInteractionType hitType)
    {
        return hitType switch
        {
            EInteractionType.Pick => "줍기",
            EInteractionType.See => "보기",
            EInteractionType.Use => "사용",
            EInteractionType.Find => "찾다",
            EInteractionType.Pull => "당기다",
            EInteractionType.Push => "밀다",
            EInteractionType.Open => "열다",
            EInteractionType.Close=> "닫다",
            EInteractionType.Press => "누르다",
            _ => "줍기"
        };
    }

    //SetUiInvisible

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



}
