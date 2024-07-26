using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ButtonColorController : MonoBehaviour
{
    private Color originColor;

    void Start()
    {
        originColor = transform.GetComponentInChildren<TextMeshProUGUI>().color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnButton()
    {
        TextMeshProUGUI uiText = transform.GetComponentInChildren<TextMeshProUGUI>();
        uiText.color = Color.white;
    }
    public void OutButton()
    {
        TextMeshProUGUI uiText = transform.GetComponentInChildren<TextMeshProUGUI>();
        uiText.color = originColor;
    }
}
