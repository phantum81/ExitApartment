using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class LightMapChanger : MonoBehaviour
{


    [Header("밤Color 라이트맵"), SerializeField]
    private Texture2D[] nightLightmapColorTextures;
    [Header("밤Dir 라이트맵"), SerializeField]
    private Texture2D[] nightLightmapDirTextures;

    [Header("이스케이프Color 라이트맵"), SerializeField]
    private Texture2D[] escapeLightmapColorTextures;
    [Header("이스케이프Dir 라이트맵"), SerializeField]
    private Texture2D[] escapeLightmapDirTextures;
    [Header("이스케이프Dir 쉐도우마스크"), SerializeField]
    private Texture2D[] escapeLightmapShadowMaskTextures;



    private string nightFolderPath = "LightMap/NightLightSet";
    private string escapeFolderPath = "LightMap/EscapeLightSet";


    private LightmapData[] nightLightData;
    private LightmapData[] escapeLightData;

    private EFloorType efloorType;
    void Start()
    {
       // Init();
       // ChangeLightmap(escapeLightData);
        
    }

    // Update is called once per frame
    void Update()
    {
        //UpdateLightMap();
    }

    void InitLightmap()
    {
        if (nightLightmapColorTextures.Length != nightLightmapDirTextures.Length || escapeLightmapColorTextures.Length != escapeLightmapDirTextures.Length)
        {
            Debug.LogError("라이트맵 텍스처의 수가 일치하지 않습니다.");
            return;
        }


        nightLightData = new LightmapData[nightLightmapColorTextures.Length];
        escapeLightData = new LightmapData[escapeLightmapColorTextures.Length];

        for (int i = 0; i < nightLightData.Length; i++)
        {
            nightLightData[i] = new LightmapData
            {
                lightmapColor = nightLightmapColorTextures[i],
                lightmapDir = nightLightmapDirTextures[i]
            };
        }

        for (int i = 0; i < escapeLightData.Length; i++)
        {
            escapeLightData[i] = new LightmapData
            {
                lightmapColor = escapeLightmapColorTextures[i],
                lightmapDir = escapeLightmapDirTextures[i],
                shadowMask = escapeLightmapShadowMaskTextures[i]
                
            };
        }
        



    }
    private void ChangeLightmap(LightmapData[] _data)
    {
        if(LightmapSettings.lightmaps != _data)
            LightmapSettings.lightmaps = _data;
    }
    void UpdateLightMap()
    {
        switch (GameManager.Instance.unitMgr.ElevatorCtr.eCurFloor)
        {
            case EFloorType.Home15EB:
            case EFloorType.Nothing436A:
            case EFloorType.Mob122F:
                ChangeLightmap(nightLightData);
                break;
            case EFloorType.Forest5ABC:
                ChangeLightmap(null);
                break;
            case EFloorType.Escape888B:
                ChangeLightmap(escapeLightData);
                break;
            case EFloorType.Looby:
                ChangeLightmap(null);
                break;

        }
    }


    private void Init()
    {
        int nightFileCount = GetFileCountInSubfolder(nightFolderPath)/2;
        int escapeFileCount = GetFileCountInSubfolder(escapeFolderPath)/3;

        nightLightmapColorTextures = new Texture2D[nightFileCount];
        nightLightmapDirTextures = new Texture2D[nightFileCount];
        escapeLightmapColorTextures = new Texture2D[escapeFileCount];
        escapeLightmapDirTextures = new Texture2D[escapeFileCount];
        escapeLightmapShadowMaskTextures = new Texture2D[escapeFileCount];

        for(int i = 0; nightLightmapColorTextures.Length> i; i++)
        {
            nightLightmapColorTextures[i] = Resources.Load<Texture2D>($"LightMap/NightLightSet/Lightmap-{i}_comp_light");
            nightLightmapDirTextures[i] = Resources.Load<Texture2D>($"LightMap/NightLightSet/Lightmap-{i}_comp_dir");
        }

        for(int i =0; escapeLightmapColorTextures.Length> i; i++)
        {
            escapeLightmapColorTextures[i] = Resources.Load<Texture2D>($"LightMap/EscapeLightSet/Lightmap-{i}_comp_light");
            escapeLightmapDirTextures[i] = Resources.Load<Texture2D>($"LightMap/EscapeLightSet/Lightmap-{i}_comp_dir");
            escapeLightmapShadowMaskTextures[i] = Resources.Load<Texture2D>($"LightMap/EscapeLightSet/Lightmap-{i}_comp_shadowmask");
        }


        InitLightmap();
    }
    int GetFileCountInSubfolder(string _path)
    {

        var resources = Resources.LoadAll(_path);
        return resources.Length;

    }
}
