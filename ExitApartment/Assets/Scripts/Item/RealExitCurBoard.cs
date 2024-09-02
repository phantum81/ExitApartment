using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class RealExitCurBoard : Item
{
    [Header("ȣ��"), SerializeField]
    private TextMeshPro addressText;
    [Header("����"), SerializeField]
    private TextMeshPro reasonText;
    [Header("����"), SerializeField]
    private TextMeshPro nameText;

    public UnityEvent onChaseBat;
    
    
    public override void Init()
    {
        base.Init();
        eItemType = EItemType.CurBoard;
        soundCtr.AudioPath = GameManager.Instance.soundMgr.SoundList[250];
    }
    public override void OnRayHit(Color _color)
    {
        
    }
    public override void OnRayOut()
    {
       
    }
    public override void OnInteraction(Vector3 _angle)
    {
        if (!GameManager.Instance.isCheckCurboard)
        {
            UiManager.Instance.inGameCtr.InGameUiShower.ErrorMessage("���� ���������...?");
            return;
        }


        if (reasonText.text.Length <= 0)
        {
            soundCtr.Play();
            addressText.text = UnitManager.ADDRESS_TEXT;
            reasonText.text = UnitManager.REASON_TEXT;
            nameText.text = UnitManager.NAME_TEXT;
            onChaseBat.Invoke();
        }

        
    }

    public override EInteractionType OnGetType()
    {
        return EInteractionType.Write;
    }
}
