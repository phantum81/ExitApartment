using NUnit.Framework.Constraints;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;

using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.Localization.Settings;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;



public class OptionView : MonoBehaviour, IOptionMenuView
{
    bool isChanging = false;
    private SoundManager soundMgr;
    private LanguageManager languageMgr;
    private InputManager inputMgr;
    [Header("감마 컨트롤러"), SerializeField]
    private GammaController gammaCtr;

    [Header("사운드 옵션버튼"), SerializeField]
    private Button soundOptionBtn;
    [Header("입력 설정 버튼"), SerializeField]
    private Button keySettingBtn;
    [Header("밝기 설정 버튼"), SerializeField]
    private Button brightOptionBtn;
    [Header("언어 버튼"), SerializeField]
    private Button languageBtn;
    [Header("나가기"), SerializeField]
    private Button exitBtn;


    [Header("전체 옵션창"), SerializeField]
    private GameObject optionPanel;
    public GameObject OptionPanel => optionPanel;

    [Header("밝기 권고메인창"), SerializeField]
    private GameObject MainrecommendPanel;

    [Header("밝기 권고창 텍스트"), SerializeField]
    private TextMeshProUGUI recommendtxt;


    [Header("미니 옵션창"), SerializeField]
    private GameObject optionMiniPanel;

    [Header("사운드옵션 판넬"), SerializeField]
    private GameObject soundOptionPanel;
    [Header("입력 세팅 판넬"), SerializeField]
    private GameObject keySettingPanel;
    [Header("밝기 판넬"), SerializeField]
    private GameObject brightOptionPanel;

    [Header("변경"), SerializeField]
    private Button[] applyBtn = new Button[3];
    [Header("취소"),SerializeField]
    private Button[] cancelBtn = new Button[3];

    [Header("배경음 슬라이드"), SerializeField]
    private Slider bgmSlider;
    [Header("효과음 슬라이드"), SerializeField]
    private Slider effectSoundSlider;
    [Header("밝기 슬라이드"), SerializeField]
    private Slider brightSlider;
    [Header("감도 슬라이드"), SerializeField]
    private Slider sensitivitySlider;


    [Header("배경음 퍼센트"), SerializeField]
    private TextMeshProUGUI bgmTxt;
    [Header("효과음 퍼센트"), SerializeField]
    private TextMeshProUGUI effectTxt;
    [Header("밝기 퍼센트"), SerializeField]
    private TextMeshProUGUI brightTxt;
    [Header("감도 퍼센트"), SerializeField]
    private TextMeshProUGUI sensitivityTxt;

    private OptionPresent optionPresent;
    
    
    void Awake()
    {

        
        optionPresent = new(GameManager.Instance.SetData, this);
        
        
    }
    void Start()
    {
        soundMgr = GameManager.Instance.soundMgr;
        inputMgr = GameManager.Instance.inputMgr;
        languageMgr = GameManager.Instance.languageMgr;
        InitButton();
        InitSlider();
        optionPresent.Init();
        // GameManager.Instance.sceneMgr.onSceneUnload += () => GameManager.Instance.sceneMgr.SetMenuObject(optionPanel, transform);
        StartCoroutine(StartGame());
    }

    // Update is called once per frame
    void Update()
    {
        if (inputMgr.InputDic[EuserAction.Ui_Menu])
        {
            EscClose();
            optionPresent.NoSaveSoundVolume();
            optionPresent.NoSaveGamma();
        }
    }

    #region 사운드 관련

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

    #region 감마관련
    public void SetGammaValue(float _value)
    {
        gammaCtr.SetGammaValue(_value);
        string value = (_value).ToString("F1");
        brightTxt.text = $"{value}";
    }

    public float GetGammaValue()
    {
        return brightSlider.value;  
    }

    public void SetGammaSliderValue(float _value)
    {
        brightSlider.value = _value;
    }

    #endregion

    #region 입력 관련
    public void SetSensitivityValue(float _value)
    {
        
        string value = (_value).ToString("F1");
        sensitivityTxt.text = $"{value}";
    }
    public float GetSensitivityValue()
    {
        return sensitivitySlider.value;
    }

    public void SetInputValue(float _value)
    {
        sensitivitySlider.value = _value;
    }
    #endregion

    #region 언어 관련
    public void SetLanguage()
    {
        int value = GetNextLanguage();
        languageMgr.SetLanguage((ELanguage)value);
        ChangeLocale(value);

    }

    public int GetNextLanguage()
    {
        int value = ((int)languageMgr.Elanguage + 1) % Enum.GetValues(typeof(ELanguage)).Length;
        
        return value;
    }
    #endregion

    public async void ChangeLocale(int _index)
    {
        if (isChanging)
            return;

        isChanging = true;

        await LocalizationSettings.InitializationOperation.Task;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[_index];

        isChanging = false;
    }


    public void CloseParentPanel()
    {


        GameObject clickObj = EventSystem.current?.currentSelectedGameObject;

        if (clickObj != null)
        {
            GameObject clickObjParent = clickObj.transform.parent?.gameObject;

            
            if (clickObjParent != null && clickObjParent.name != "MenuPan" && clickObj.name != "MenuPan")
            {
                clickObjParent.SetActive(false);
            }
        }

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

    private void EscClose()
    {
        brightOptionPanel.SetActive(false);
        keySettingPanel.SetActive(false);
        soundOptionPanel.SetActive(false);
        optionMiniPanel.SetActive(false);
        
    }


    private void InitButton()
    {
        exitBtn.onClick.AddListener(CloseParentPanel);
        soundOptionBtn.onClick.AddListener(ShowSoundOption);
        keySettingBtn.onClick.AddListener(ShowKeySettingOption);
        brightOptionBtn.onClick.AddListener(ShowBrightOption);
        languageBtn.onClick.AddListener(SetLanguage);

        AddButtonArrayListener(applyBtn, optionPresent.SaveSoundVolume, optionPresent.SaveGamma, optionPresent.SaveInput);
        AddButtonArrayListener(cancelBtn, optionPresent.NoSaveSoundVolume, optionPresent.NoSaveGamma, optionPresent.NoSaveInput);
    }

    private void InitSlider()
    {
        bgmSlider.onValueChanged.AddListener(optionPresent.SetBgm);
        effectSoundSlider.onValueChanged.AddListener(optionPresent.SetEffect);
        brightSlider.onValueChanged.AddListener(optionPresent.SetGamma);
        sensitivitySlider.onValueChanged.AddListener(optionPresent.SetSensitivity);

    }

    public void LoadData(float _bgm, float _effect, float _gamma, float _sensitivity)
    {
        bgmSlider.value = _bgm;
        effectSoundSlider.value = _effect;
        brightSlider.value = _gamma;
        sensitivitySlider.value = _sensitivity;
    }



    void AddButtonArrayListener(Button[] _buttons, Action _saveSoundVolume, Action _saveGamma, Action _saveInput)
    {
        for (int i = 0; i < _buttons.Length; i++)
        {
            EUiButtonType buttonType = _buttons[i].GetComponent<UiButtonType>().eUiButtonType;

            switch (buttonType)
            {
                case EUiButtonType.Sound:
                    _buttons[i].onClick.AddListener(() => _saveSoundVolume());
                    break;
                case EUiButtonType.Input:
                    _buttons[i].onClick.AddListener(() => _saveInput());
                    break;
                case EUiButtonType.Gamma:
                    _buttons[i].onClick.AddListener(() => _saveGamma());
                    break;
            }
        }
    }



    private IEnumerator StartGame()
    {
        //Time.timeScale = 0f;
        yield return UiManager.Instance.SetUiVisible(recommendtxt.transform, 2f, 0.2f);
        yield return UiManager.Instance.SetUiInvisible(recommendtxt.transform, 2f, 1f);
        yield return UiManager.Instance.SetUiInvisible(MainrecommendPanel.transform, 2f, 1f);


        //Time.timeScale = 1;
    }
}
