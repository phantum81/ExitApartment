using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class RealExitCurBoard : Item
{
    [Header("호수"), SerializeField]
    private TextMeshPro addressText;
    [Header("이유"), SerializeField]
    private TextMeshPro reasonText;
    [Header("서명"), SerializeField]
    private TextMeshPro nameText;

    public UnityEvent onChaseBat;
    public override void Init()
    {
        base.Init();
        eItemType = EItemType.CurBoard;
    }
    public override void OnInteraction(Vector3 _angle)
    {
        if (reasonText.text.Length <= 0)
        {
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
