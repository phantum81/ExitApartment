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

    [Header("���� ��Ʈ�ѷ�"), SerializeField]
    private GammaController gammaCtr;

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

    private OptionPresent optionPresent;
    void Awake()
    {
        soundMgr = GameManager.Instance.soundMgr;
        optionPresent = new(GameManager.Instance.SetData, this);


        InitButton();
        InitSlider();
        optionPresent.Init();
        
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region ���� ����

    public (float, float) GetSoundVolume()
    {
        return (bgmSlider.value, effectSoundSlider.value);
    }

    public void SetSoundVolume(float _bgm, float _effecet)
    {
        bgmSlider.value = _bgm;
        effectSoundSlider.value = _effecet;
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

    #endregion


    public void SetGammaValue(float _value)
    {
        gammaCtr.SetGammaValue(_value);
        string value = (_value).ToString("F1");
        brightTxt.text = $"{value}";
    }

    public float GetGammaValue()
    {
        return gammaCtr.GetGammaValue();  
    }

    public void CloseParentPanel()
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




    private void InitButton()
    {
        exitBtn.onClick.AddListener(CloseParentPanel);
        soundOptionBtn.onClick.AddListener(ShowSoundOption);
        keySettingBtn.onClick.AddListener(ShowKeySettingOption);
        brightOptionBtn.onClick.AddListener(ShowBrightOption);
        for(int i = 0; i < applyBtn.Length; i++)
        {
            
            applyBtn[i].onClick.AddListener(optionPresent.SaveSoundVolume);
            cancelBtn[i].onClick.AddListener(optionPresent.NoSaveSoundVolume);
        }
    }

    private void InitSlider()
    {
        bgmSlider.onValueChanged.AddListener(optionPresent.SetBgm);
        effectSoundSlider.onValueChanged.AddListener(optionPresent.SetEffect);
        brightSlider.onValueChanged.AddListener(optionPresent.SetGamma);
    }

    public void LoadData(float _bgm, float _effect)
    {
        bgmSlider.value = _bgm;
        effectSoundSlider.value = _effect;
    }
}
