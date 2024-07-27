using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuData
{
    public GameData data;
    public MenuData(GameData data)
    {
        this.data = data;
    }
    public void DataReset()
    {
        data.eFloorData = EFloorType.Home15EB;
        data.isPumpkinEvent = false;
    }

}
