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



    void Start()
    {
        elevatorCtr = GameManager.Instance.unitMgr.ElevatorCtr;
        playerInteraction = GameManager.Instance.unitMgr.PlayerCtr.gameObject.GetComponent<PlayerInteraction>();
        inputMgr = GameManager.Instance.inputMgr;
        GameManager.Instance.onGetForestHumanity += ActiveHumanity;
        StartCoroutine(ResetScreen());
    }

    // Update is called once per frame
    void Update()
    {
        ePlayerState = HFSM<EplayerState, PlayerPostProcess>.Instance.CurState;



        if (inputMgr.InputDic[EuserAction.Inventory])
        {            
            ActiveUi(!inventoryPan.activeSelf, inventoryPan);
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
            StartCoroutine(SetUiInvisible(UiHumanityList[i].transform, 2f, 2f));
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
            ReWrite(elevatorError_txt, "같은 층 이다..");
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
                        StartCoroutine(SetUiInvisible(elevatorError_txt.transform, 2f, 2f));
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
                        StartCoroutine(SetUiInvisible(elevatorError_txt.transform, 2f, 2f));
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
                        StartCoroutine(SetUiInvisible(elevatorError_txt.transform, 2f, 2f));
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
                        StartCoroutine(SetUiInvisible(elevatorError_txt.transform, 2f, 2f));
                    }
                    return false;
                case EFloorType.Escape888B:
                    if (!elevatorError_txt.gameObject.activeSelf)
                    {
                        ActiveUi(true, elevatorError_txt.gameObject);
                        ReWrite(elevatorError_txt, "갈 수 없는 듯 하다...");
                        StartCoroutine(SetUiInvisible(elevatorError_txt.transform, 2f, 2f));
                    }
                    return false;

                 default:
                    if (!elevatorError_txt.gameObject.activeSelf)
                    {
                        ActiveUi(true, elevatorError_txt.gameObject);
                        ReWrite(elevatorError_txt, "갈 수 없는 듯 하다...");
                        StartCoroutine(SetUiInvisible(elevatorError_txt.transform, 2f, 2f));
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
            yield return StartCoroutine(SetUiVisible(diePanel.transform, 1f, 1f));
            GameManager.Instance.Restart();
        }
    }


    public IEnumerator SetUiInvisible(Transform _target, float _time, float _wait) 
    {
        yield return new WaitForSeconds(_wait);
        float curAlpha = 1f; // 최대 투명도로 시작
        Color curColor;

        // UI 요소의 컬러 컴포넌트를 가져옴
        Graphic uiGraphic = _target.GetComponent<Image>();
        TextMeshProUGUI uiText = _target.GetComponent<TextMeshProUGUI>();

        if (uiGraphic != null)
        {
            curColor = uiGraphic.color;
        }
        else if (uiText != null)
        {
            curColor = uiText.color;
        }
        else
        {
            // UI 요소가 Image나 TextMeshProUGUI 컴포넌트를 가지고 있지 않으면 함수 종료
            yield break;
        }

        // 투명도를 서서히 줄이면서 UI를 투명하게 만듦
        while (curAlpha > 0f)
        {
            curAlpha -= Time.deltaTime / _time; // _time 동안에 투명도를 줄임
            curColor.a = curAlpha; // 컬러의 알파 채널을 갱신

            if (uiGraphic != null)
            {
                uiGraphic.color = curColor;
            }
            else if (uiText != null)
            {
                uiText.color = curColor;
            }

            yield return null;
        }

        // 투명도가 0 미만으로 내려가는 것을 방지하기 위해 0으로 설정
        curColor.a = 1f;

        // 마지막에 UI 요소의 투명도를 완전히 0으로 설정
        if (uiGraphic != null)
        {
            uiGraphic.color = curColor;
        }
        else if (uiText != null)
        {
            uiText.color = curColor;
        }

        ActiveUi(false, _target.gameObject);

    }

    public IEnumerator SetUiVisible(Transform _target, float _time, float _wait)
    {
        yield return new WaitForSeconds(_wait);
        float curAlpha = 0f; // 최소 투명도로 시작
        Color curColor;

        // UI 요소의 컬러 컴포넌트를 가져옴
        Graphic uiGraphic = _target.GetComponent<Image>();
        TextMeshProUGUI uiText = _target.GetComponent<TextMeshProUGUI>();

        if (uiGraphic != null)
        {
            curColor = uiGraphic.color;
        }
        else if (uiText != null)
        {
            curColor = uiText.color;
        }
        else
        {
            // UI 요소가 Image나 TextMeshProUGUI 컴포넌트를 가지고 있지 않으면 함수 종료
            yield break;
        }

        // 투명도를 서서히 높이면서 UI를 나타나게 함
        while (curAlpha < 1f)
        {
            curAlpha += Time.deltaTime / _time; // _time 동안에 투명도를 높임
            curColor.a = curAlpha; // 컬러의 알파 채널을 갱신

            if (uiGraphic != null)
            {
                uiGraphic.color = curColor;
            }
            else if (uiText != null)
            {
                uiText.color = curColor;
            }

            yield return null;
        }

        // 투명도가 1을 넘지 않도록 1로 설정
        curColor.a = 1f;

        // 마지막에 UI 요소의 투명도를 완전히 1로 설정
        if (uiGraphic != null)
        {
            uiGraphic.color = curColor;
        }
        else if (uiText != null)
        {
            uiText.color = curColor;
        }

        ActiveUi(true, _target.gameObject);
    }

}
