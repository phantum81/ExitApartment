using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorDoorButton : MonoBehaviour, IInteraction
{
    public EElevatorButtonType buttonType;
    private Color originColor;
    private Material curMaterial;
    
    public void OnRayHit(Color _color)
    {
        curMaterial.color = _color;

    }
    public void OnInteraction()
    {
        if(buttonType == EElevatorButtonType.Open && !GameManager.Instance.unitMgr.ElevatorCtr.IsOpen)
        {            
            StartCoroutine(GameManager.Instance.unitMgr.ElevatorCtr.OpenDoor());
        }
        else if(buttonType == EElevatorButtonType.Close && GameManager.Instance.unitMgr.ElevatorCtr.IsOpen)
            StartCoroutine(GameManager.Instance.unitMgr.ElevatorCtr.CloseDoor());
        // 조건 생각해야함.

    }
    public void OnRayOut()
    {
        curMaterial.color = originColor;
    }
}
