using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InGameUiShower : MonoBehaviour
{
    [Header("픽 마크"),SerializeField]
    private GameObject pickMark;
    [Header("인벤토리 창"), SerializeField]
    private GameObject inventoryPan;
    [Header("층 입력 텍스트"), SerializeField]
    private TextMeshPro writeFloor_txt;
    [Header("현재 층 텍스트"), SerializeField]
    private TextMeshPro curFloor_txt;

    private InputManager inputMgr;
   
    private PlayerInteraction playerInteraction;
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

    
    
}
