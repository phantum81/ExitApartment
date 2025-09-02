using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InputUiKeyBindingController : MonoBehaviour
{
    private InputKeyButton waitingButton;
    private bool isWaitingForKey = false;
    public bool IsWaitingForKey => isWaitingForKey;

    private InputBinding viewerInputBinding;
    
    public GameObject playerGridPanel;
    public GameObject itemGridPanel;
    public GameObject pressKeyPanel ;
    private List<InputKeyButton> allButtons = new List<InputKeyButton>();
    void Start()
    {


        allButtons.AddRange(playerGridPanel.GetComponentsInChildren<InputKeyButton>());
        allButtons.AddRange(itemGridPanel.GetComponentsInChildren<InputKeyButton>());

        LoadOriginInputBinding();

        foreach (var btn in allButtons)
        {
            btn.UpdateText(viewerInputBinding.BindingDic[btn.eAction].ToString());
            RegisterButton(btn);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isWaitingForKey)
        {
            InputKey();
        }
    }

    private void InputKey()
    {
        foreach (KeyCode key in System.Enum.GetValues(typeof(KeyCode)))
        {

            if (Input.GetKeyDown(key))
            {
                if(key == KeyCode.Escape)
                {

                    return;
                }
                viewerInputBinding.Bind(waitingButton.eAction, key);
                UpdateAllButtonTexts();
                isWaitingForKey = false;
                pressKeyPanel.SetActive(false);
                break;
            }
        }
    }
    private void OnKeyButtonClicked(EuserAction _action)
    {
        waitingButton = FindButton(_action);
        isWaitingForKey = true;        
        pressKeyPanel.SetActive(true);
    }
    public void RegisterButton(InputKeyButton _btn)
    {
        _btn.onClickEvent.AddListener(OnKeyButtonClicked);
    }

    private InputKeyButton FindButton(EuserAction _action)
    {
        return allButtons.Find(btn => btn.eAction == _action);
    }

    public void UpdateAllButtonTexts()
    {
        foreach (var btn in allButtons)
        {
            btn.UpdateText(viewerInputBinding.BindingDic[btn.eAction].ToString());

        }
    }

    public void LoadOriginInputBinding()
    {
        viewerInputBinding = new InputBinding(new SerializableInputBinding(GameManager.Instance.inputMgr.Inputbind));
    }


    public void SaveInputBinding()
    {
        GameManager.Instance.inputMgr.Inputbind.SaveFile(new SerializableInputBinding (viewerInputBinding));
    }

    public void InitViewerInputBinding()
    {
        viewerInputBinding.ResetAll();
    }


}
