using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "ScriptableData/GameData", order = 1)]
public class GameData : ScriptableObject
{
    public string data;
    [Header("Ãþ ÀúÀå")]
    public EFloorType eFloorData;
    public string sceneToLoad;
    public bool isPumpkinEvent = false;


    public void ResetData()
    {
        eFloorData = EFloorType.Home15EB;
        isPumpkinEvent= false;

        SaveData(eFloorData, isPumpkinEvent);
    }

    public void SaveData(EFloorType _type, bool _bol)
    {
        int ispump = 0;
        if(_bol)
        {
            ispump = 1;
        }
        
        PlayerPrefs.SetInt("EFloorType", (int)_type);
        PlayerPrefs.SetInt("IsPumpKinEvent", ispump);
        PlayerPrefs.Save();
    }

    public void LoadData()
    {
        eFloorData = (EFloorType)PlayerPrefs.GetInt("EFloorType");
        if (PlayerPrefs.GetInt("IsPumpKinEvent") == 0)
        {
            isPumpkinEvent = false;
        }
        else
            isPumpkinEvent= true;
        
    }
}
