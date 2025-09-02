using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InputKeyButton : MonoBehaviour
{
    public EuserAction eAction;                
    public KeyButtonClickEvent onClickEvent;  

    private Button button;
    
    void Awake()
    {
        
        button.onClick.AddListener(() => onClickEvent.Invoke(eAction));
    }


    public void UpdateText(string _txt)
    {
        button = GetComponent<Button>();
        button.GetComponentInChildren<TextMeshProUGUI>().text = _txt;
    }
   
}

[System.Serializable]
public class KeyButtonClickEvent : UnityEvent<EuserAction> { }