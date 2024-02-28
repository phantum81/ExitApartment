using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorDoorButton : MonoBehaviour, IInteraction
{
    public EElevatorButtonType buttonType;
    
    private Color originColor;
    private Material curMaterial;
    private ElevatorController eleCtr;




    public void OnRayHit(Color _color)
    {
        curMaterial.color = _color;

    }
    public void OnInteraction()
    {
        if (buttonType == EElevatorButtonType.Open)
        {

            if (eleCtr.eleWork == EElevatorWork.Closing && eleCtr.CurCoroutine != null)
            {
                eleCtr.StopCoroutine(eleCtr.CurCoroutine);
            }                     
                eleCtr.eleWork = EElevatorWork.Opening;
                eleCtr.CurCoroutine = eleCtr.StartCoroutine(eleCtr.OpenDoor());

        }
        else if (buttonType == EElevatorButtonType.Close)
        {

            if (eleCtr.eleWork == EElevatorWork.Opening && eleCtr.CurCoroutine != null)
            {
                eleCtr.StopCoroutine(eleCtr.CurCoroutine);
            }
                eleCtr.eleWork = EElevatorWork.Closing;

                eleCtr.CurCoroutine = eleCtr.StartCoroutine(eleCtr.CloseDoor());
        }

    }
    public void OnRayOut()
    {
        curMaterial.color = originColor;
    }

    public void Init()
    {
        GameManager.Instance.itemMgr.InitInteractionItem(out curMaterial, out originColor, transform);
        eleCtr = GameManager.Instance.unitMgr.ElevatorCtr;
    }

}
