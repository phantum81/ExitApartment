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

}
