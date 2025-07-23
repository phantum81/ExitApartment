using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AchievementData", menuName = "ScriptableData/AchievementData")]
public class AchievementData : ScriptableObject
{
    [SerializeField]
    private int dieCount;
    public int DieCount => dieCount;


    public void SaveData(int _count)
    {

        dieCount = _count;
        PlayerPrefs.SetInt("DieCount", dieCount);
        PlayerPrefs.Save();
    }

    public void LoadData()
    {
        dieCount = PlayerPrefs.GetInt("DieCount", 0);
    }
}
