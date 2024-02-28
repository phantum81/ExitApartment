using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ElevatorPan : MonoBehaviour, IInteraction
{
    private Color originColor;
    private Material curMaterial;
    


    public void OnRayHit( Color _color)
    {
        curMaterial.color = _color;

    }
    public void OnInteraction()
    {
        ElevatorNumData data = GameManager.Instance.itemMgr.ElevatorFloorDic[transform.GetComponentInChildren<ElevatorPan>()];
        UiManager.Instance.inGameCtr.InGameUiShower.RenewWriteFloor(data.Num);
    }
    public void OnRayOut()
    {
        curMaterial.color = originColor;
    }
    public void Init()
    {
        GameManager.Instance.itemMgr.InitInteractionItem(out curMaterial, out originColor, transform);
    }
}
