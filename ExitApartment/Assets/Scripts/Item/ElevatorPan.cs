using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ElevatorPan : MonoBehaviour, IInteraction
{
    private List<Color> originColor = new List<Color>();
    private List<Material> curMaterial = new List<Material>();

    private SoundController soundCtr;

    public void OnRayHit( Color _color)
    {
        foreach (Material mat in curMaterial)
        {
            mat.color = _color;
        }
        

    }
    public void OnInteraction(Vector3 _angle)
    {
        ElevatorNumData data = GameManager.Instance.itemMgr.ElevatorFloorDic[transform.GetComponentInChildren<ElevatorPan>()];
        UiManager.Instance.inGameCtr.InGameUiShower.RenewWriteFloor(data.Num);
        
        soundCtr.Play();
    }
    public EInteractionType OnGetType()
    {
        return EInteractionType.Press;
    }
    public void OnRayOut()
    {
        for(int i = 0; i < curMaterial.Count; i++)
        {
            curMaterial[i].color = originColor[i];
        }
        
    }
    public void Init()
    {
        GameManager.Instance.itemMgr.InitInteractionItem(ref curMaterial, ref originColor, transform);
        soundCtr = GetComponent<SoundController>();
        soundCtr.AudioPath = GameManager.Instance.soundMgr.SoundList[34];
    }
}
