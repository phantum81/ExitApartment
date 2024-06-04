using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class DirectLight : MonoBehaviour
{
    private Light directLight;
    private Color originColor;
    private void Start()
    {
        
        directLight = GetComponent<Light>();
        originColor = directLight.color;
    }
    public void ChangeLightColor()
    {
        StartCoroutine(GameManager.Instance.unitMgr.CorChangeLightColor(directLight, originColor, Color.red, 4f));

    }




    void OnApplicationQuit()
    {
        directLight.color = originColor;
    }

}
