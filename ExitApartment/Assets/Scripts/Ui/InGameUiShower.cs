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

    private InputManager inputMgr;
    string interactionAction;
    private PlayerInteraction playerInteraction;

    private EInteractionType hitType;




    void Start()
    {
        GameManager.Instance.onGetForestHumanity += ActiveHumanity;
        playerInteraction = GameManager.Instance.unitMgr.PlayerCtr.gameObject.GetComponent<PlayerInteraction>();
        inputMgr = GameManager.Instance.inputMgr;
    }

    // Update is called once per frame
    void Update()
    {
        

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

    public void RenewCurFloor(string _txt)
    {
        curFloor_txt.text = _txt;
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
}
