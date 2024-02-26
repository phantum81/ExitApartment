using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InGameUiShower : MonoBehaviour
{
    [Header("픽 마크"),SerializeField]
    private GameObject pickMark;
    [Header("층 입력 텍스트"), SerializeField]
    private TextMeshPro writeFloor_txt;
    [Header("현재 층 텍스트"), SerializeField]
    private TextMeshPro curFloor_txt;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivePickUpMark(bool _bool)
    {
        pickMark.SetActive(_bool);
    }

    public void RenewWriteFloor(string _txt)
    {
        if (writeFloor_txt.GetTextInfo(writeFloor_txt.text).characterCount < 4)
            writeFloor_txt.text += _txt;
        else
            writeFloor_txt.text = string.Empty;
    }

    public void RenewCurFloor(string _txt)
    {
        curFloor_txt.text = _txt;
    }

    
}
