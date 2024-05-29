using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ElevatorDoorButton : MonoBehaviour, IInteraction
{
    public EElevatorButtonType buttonType;

    private List<Color> originColor = new List<Color>();
    private List<Material> curMaterial = new List<Material>();
    private ElevatorController eleCtr;



    public void OnRayHit(Color _color)
    {
        foreach (Material mat in curMaterial)
        {
            mat.color = _color;
        }

    }

    public void OnInteraction(Vector3 _angle)
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

    public EInteractionType OnGetType()
    {
        return EInteractionType.Press;
    }

    public void OnRayOut()
    {
        for (int i = 0; i < curMaterial.Count; i++)
        {
            curMaterial[i].color = originColor[i];
        }
    }

    public void Init()
    {
        GameManager.Instance.itemMgr.InitInteractionItem(ref curMaterial, ref originColor, transform);
        eleCtr = GameManager.Instance.unitMgr.ElevatorCtr;
    }

}
