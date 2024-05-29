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

    private InputManager inputMgr;
    string interactionAction;
    private PlayerInteraction playerInteraction;

    private EInteractionType hitType;
    void Start()
    {
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
}
