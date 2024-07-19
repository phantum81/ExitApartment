using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class InGameUiShower : MonoBehaviour
{
    [Header("��ȣ�ۿ� ��ũ"),SerializeField]
    private GameObject pickMark;
    [Header("��ȣ�ۿ� �ؽ�Ʈ"), SerializeField]
    private TextMeshProUGUI interactionTxt;

    [Header("���� �ǳ�"), SerializeField]
    private GameObject diePanel;



    [Header("�κ��丮 â"), SerializeField]
    private GameObject inventoryPan;
    [Header("�� �Է� �ؽ�Ʈ"), SerializeField]
    private TextMeshPro writeFloor_txt;
    [Header("���� �� �ؽ�Ʈ"), SerializeField]
    private TextMeshPro curFloor_txt;

    [Header("�ΰ��� ����Ʈ"), SerializeField]
    private List<GameObject> UiHumanityList = new List<GameObject>();

    [Header("�ΰ��� �ؽ�Ʈ"), SerializeField]
    private TextMeshProUGUI humanityText;


    [Header("���������� ���� ����"), SerializeField]
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
            ReWrite(elevatorError_txt, "���� �� �̴�..");
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
                        ReWrite(elevatorError_txt, "�� �� ���� �� �ϴ�...");
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
                        ReWrite(elevatorError_txt, "�� �� ���� �� �ϴ�...");
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
                        ReWrite(elevatorError_txt, "�� �� ���� �� �ϴ�...");
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
                        ReWrite(elevatorError_txt, "�� �� ���� �� �ϴ�...");
                        StartCoroutine(SetUiInvisible(elevatorError_txt.transform, 2f, 2f));
                    }
                    return false;
                case EFloorType.Escape888B:
                    if (!elevatorError_txt.gameObject.activeSelf)
                    {
                        ActiveUi(true, elevatorError_txt.gameObject);
                        ReWrite(elevatorError_txt, "�� �� ���� �� �ϴ�...");
                        StartCoroutine(SetUiInvisible(elevatorError_txt.transform, 2f, 2f));
                    }
                    return false;

                 default:
                    if (!elevatorError_txt.gameObject.activeSelf)
                    {
                        ActiveUi(true, elevatorError_txt.gameObject);
                        ReWrite(elevatorError_txt, "�� �� ���� �� �ϴ�...");
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
            EInteractionType.Pick => "�ݱ�",
            EInteractionType.See => "����",
            EInteractionType.Use => "���",
            EInteractionType.Find => "ã��",
            EInteractionType.Pull => "����",
            EInteractionType.Push => "�д�",
            EInteractionType.Open => "����",
            EInteractionType.Close=> "�ݴ�",
            EInteractionType.Press => "������",
            _ => "�ݱ�"
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
        float curAlpha = 1f; // �ִ� ������ ����
        Color curColor;

        // UI ����� �÷� ������Ʈ�� ������
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
            // UI ��Ұ� Image�� TextMeshProUGUI ������Ʈ�� ������ ���� ������ �Լ� ����
            yield break;
        }

        // ������ ������ ���̸鼭 UI�� �����ϰ� ����
        while (curAlpha > 0f)
        {
            curAlpha -= Time.deltaTime / _time; // _time ���ȿ� ������ ����
            curColor.a = curAlpha; // �÷��� ���� ä���� ����

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

        // ������ 0 �̸����� �������� ���� �����ϱ� ���� 0���� ����
        curColor.a = 1f;

        // �������� UI ����� ������ ������ 0���� ����
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
        float curAlpha = 0f; // �ּ� ������ ����
        Color curColor;

        // UI ����� �÷� ������Ʈ�� ������
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
            // UI ��Ұ� Image�� TextMeshProUGUI ������Ʈ�� ������ ���� ������ �Լ� ����
            yield break;
        }

        // ������ ������ ���̸鼭 UI�� ��Ÿ���� ��
        while (curAlpha < 1f)
        {
            curAlpha += Time.deltaTime / _time; // _time ���ȿ� ������ ����
            curColor.a = curAlpha; // �÷��� ���� ä���� ����

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

        // ������ 1�� ���� �ʵ��� 1�� ����
        curColor.a = 1f;

        // �������� UI ����� ������ ������ 1�� ����
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
