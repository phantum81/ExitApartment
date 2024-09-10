using NUnit.Framework.Constraints;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;


public class OptionView : MonoBehaviour, IOptionMenuView
{

    SoundManager soundMgr;
    [Header("���� �ɼǹ�ư"), SerializeField]
    private Button soundOptionBtn;
    [Header("�Է� ���� ��ư"), SerializeField]
    private Button keySettingBtn;
    [Header("��� ���� ��ư"), SerializeField]
    private Button brightOptionBtn;
    [Header("������"), SerializeField]
    private Button exitBtn;



    [Header("����ɼ� �ǳ�"), SerializeField]
    private GameObject soundOptionPanel;
    [Header("Ű ���� �ǳ�"), SerializeField]
    private GameObject keySettingPanel;
    [Header("��� �ǳ�"), SerializeField]
    private GameObject brightOptionPanel;

    [Header("����"), SerializeField]
    private Button[] applyBtn = new Button[3];
    [Header("���"),SerializeField]
    private Button[] cancelBtn = new Button[3];
    [Header("����� �����̵�"), SerializeField]
    private Slider bgmSlider;
    [Header("ȿ���� �����̵�"), SerializeField]
    private Slider effectSoundSlider;
    [Header("��� �����̵�"), SerializeField]
    private Slider brightSlider;
    [Header("����� �ۼ�Ʈ"), SerializeField]
    private TextMeshProUGUI bgmTxt;
    [Header("ȿ���� �ۼ�Ʈ"), SerializeField]
    private TextMeshProUGUI effectTxt;
    [Header("��� �ۼ�Ʈ"), SerializeField]
    private TextMeshProUGUI brightTxt;


    private float originBrightValue;
    private float originEffectSoundValue = 0.5f;
    private float originBgmSoundValue = 0.5f;

    void Awake()
    {
        soundMgr = GameManager.Instance.soundMgr;

        InitButton();
        InitSlider();

        bgmSlider.value = originBgmSoundValue;
        effectSoundSlider.value = originEffectSoundValue;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetBgmVolume(float _value)
    {
        soundMgr.SetBgmVolume(_value);
        string value = (_value * 100f).ToString("F0");
        bgmTxt.text = $"{value}%";
    }

    public void SetEffectVolume(float _value)
    {
        soundMgr.SetEffectVolume(_value);
        string value = (_value * 100f).ToString("F0");
        effectTxt.text = $"{value}%";
    }

    private void CloseParentPanel()
    {
        GameObject clickObjParent = EventSystem.current.currentSelectedGameObject.transform.parent.gameObject;
        clickObjParent.SetActive(false);
    }
    private void ShowSoundOption()
    {
        soundOptionPanel.SetActive(true);
    }
    private void ShowKeySettingOption()
    {
        keySettingPanel.SetActive(true);
    }
    private void ShowBrightOption()
    {
        brightOptionPanel.SetActive(true);
    }

    private void SaveValue()
    {
        originBgmSoundValue = bgmSlider.value;
        originEffectSoundValue = effectSoundSlider.value;

    }

    private void ReturnValue()
    {
        bgmSlider.value = originBgmSoundValue;
        effectSoundSlider.value = originEffectSoundValue;

    }
    private void InitButton()
    {
        exitBtn.onClick.AddListener(CloseParentPanel);
        soundOptionBtn.onClick.AddListener(ShowSoundOption);
        keySettingBtn.onClick.AddListener(ShowKeySettingOption);
        brightOptionBtn.onClick.AddListener(ShowBrightOption);
        for(int i = 0; i < applyBtn.Length; i++)
        {
            applyBtn[i].onClick.AddListener(SaveValue);
            cancelBtn[i].onClick.AddListener(ReturnValue);
        }
    }

    private void InitSlider()
    {
        bgmSlider.onValueChanged.AddListener(SetBgmVolume);
        effectSoundSlider.onValueChanged.AddListener(SetEffectVolume);
    }
}
