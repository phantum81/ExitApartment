using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ElevatorDoorButton : MonoBehaviour, IInteraction
{
    public EElevatorButtonType buttonType;

    private List<Color> originColor = new List<Color>();
    private List<Material> curMaterial = new List<Material>();
    private ElevatorController eleCtr;
    private SoundController soundCtr;
    private InGameUiShower uiShower;
    private CameraManager cameraMgr;
    private UnitManager unitMgr;
    private bool isLock = false;
    


    public void OnRayHit(Color _color)
    {
        foreach (Material mat in curMaterial)
        {
            mat.color = _color;
        }

    }

    public void OnInteraction(Vector3 _angle)
    {
        soundCtr.Play();
        if (buttonType == EElevatorButtonType.Open && eleCtr.eleWork != EElevatorWork.Locking)
        {
            if (GameManager.Instance.isClear12F || GameManager.Instance.isClearForest )
            {
                return;
            }
            if (eleCtr.eCurFloor == EFloorType.Looby)
            {
                cameraMgr.ChangeCamera(cameraMgr.CameraDic[4]);
                unitMgr.PlayerCtr.gameObject.SetActive(false);
                unitMgr.LobbyPlayer.gameObject.SetActive(true);
               

            }

            if (eleCtr.eleWork == EElevatorWork.Closing && eleCtr.CurCoroutine != null)
            {
                eleCtr.StopCoroutine(eleCtr.CurCoroutine);
                eleCtr.CurCoroutine = null;
            }
            if(eleCtr.CurCoroutine == null)
            {

                eleCtr.eleWork = EElevatorWork.Opening;
                eleCtr.CurCoroutine = eleCtr.StartCoroutine(eleCtr.OpenDoor());
            }
            
            

        }
        else if (buttonType == EElevatorButtonType.Close && eleCtr.eleWork != EElevatorWork.Locking)
        {
            
            if (eleCtr.eleWork == EElevatorWork.Opening && eleCtr.CurCoroutine != null)
            {
                eleCtr.StopCoroutine(eleCtr.CurCoroutine);
                eleCtr.CurCoroutine = null;
            }

            if(eleCtr.CurCoroutine == null)
            {
                eleCtr.eleWork = EElevatorWork.Closing;
                eleCtr.CurCoroutine = eleCtr.StartCoroutine(eleCtr.CloseDoor());
            }




        }
        else if (buttonType == EElevatorButtonType.Move)
        {
            
            if (uiShower.CheckRightFloor() && eleCtr.IsClose)
            {
                if (GameManager.Instance.isClearEscapeRoom)
                {                    
                    GameManager.Instance.SetEscapeClearFloor(false);
                }
                if (GameManager.Instance.isClearForest)
                {
                    GameManager.Instance.ChangeFloorLevel(EFloorType.Forest5ABC);
                    GameManager.Instance.SetForestClearFloor(false);
                }
                if (GameManager.Instance.isClear12F)
                {
                    
                    GameManager.Instance.Set12FClearFloor(false);
                }


                if (eleCtr.CurCoroutine != null)
                {
                    eleCtr.StopCoroutine(eleCtr.CurCoroutine);
                    eleCtr.CurCoroutine = null;
                }

                
                if (eleCtr.CurCoroutine == null)
                {
                    eleCtr.eleWork = EElevatorWork.Locking;
                    if(UiManager.Instance.inGameCtr.InGameUiShower.GetCurFloor() == UnitManager.ESCAPE_FLOOR)
                        UiManager.Instance.inGameCtr.InGameUiShower.ScreenChange(1f, 1f);
                    eleCtr.CurCoroutine = eleCtr.StartCoroutine(eleCtr.MoveFloor());
                }

            }
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
        soundCtr = gameObject.GetComponent<SoundController>();
        soundCtr.AudioPath = GameManager.Instance.soundMgr.SoundList[34];
        uiShower = UiManager.Instance.inGameCtr.InGameUiShower;
        cameraMgr = GameManager.Instance.cameraMgr;
        unitMgr = GameManager.Instance.unitMgr;

    }

}
