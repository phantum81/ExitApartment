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

    [Header("감마 컨트롤러"), SerializeField]
    private GammaController gammaCtr;

    [Header("사운드 옵션버튼"), SerializeField]
    private Button soundOptionBtn;
    [Header("입력 설정 버튼"), SerializeField]
    private Button keySettingBtn;
    [Header("밝기 설정 버튼"), SerializeField]
    private Button brightOptionBtn;
    [Header("나가기"), SerializeField]
    private Button exitBtn;



    [Header("사운드옵션 판넬"), SerializeField]
    private GameObject soundOptionPanel;
    [Header("키 세팅 판넬"), SerializeField]
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
    [Header("배경음 퍼센트"), SerializeField]
    private TextMeshProUGUI bgmTxt;
    [Header("효과음 퍼센트"), SerializeField]
    private TextMeshProUGUI effectTxt;
    [Header("밝기 퍼센트"), SerializeField]
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
