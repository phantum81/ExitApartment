using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InGameUiShower : MonoBehaviour
{
    [Header("�� ��ũ"),SerializeField]
    private GameObject pickMark;
    [Header("�� �Է� �ؽ�Ʈ"), SerializeField]
    private TextMeshPro writeFloor_txt;
    [Header("���� �� �ؽ�Ʈ"), SerializeField]
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

    public void RenewCurFloor(string _txt)
    {
        writeFloor_txt.text = _txt;
    }

    
}
