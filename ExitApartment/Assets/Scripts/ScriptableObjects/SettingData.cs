using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SettingData", menuName = "ScriptableData/SettingData")]
public class SettingData : ScriptableObject
{
    [SerializeField]
    private float bgmValue;
    public float BgmValue => bgmValue;
    [SerializeField]
    private float effectSoundValue;
    public float EffectSoundValue => effectSoundValue;
    [SerializeField]
    private float gammaValue;
    public float GammaValue => gammaValue;

    [SerializeField]
    private float sensitivity;
    public float Sensitivity => sensitivity;

    [SerializeField]
    private bool isStart;
    public bool IsStart => isStart;

    [SerializeField]
    private ELanguage eLanguage;
    public ELanguage ELanguage => eLanguage;


    public void SetBgmData(float _bgm)
    {
        bgmValue= _bgm;
        PlayerPrefs.SetFloat("bgmValue", _bgm);
        
    }
    public void SetEffectData(float _effect)
    {
        effectSoundValue = _effect;
        PlayerPrefs.SetFloat("effectSoundValue", _effect);
    }

    public void SetGammaData(float _value)
    {
        gammaValue= _value;
        PlayerPrefs.SetFloat("gammaValue", _value);
    }
    public void SetSensitivityData(float _value)
    {
        sensitivity = _value;
        PlayerPrefs.SetFloat("sensitivity", _value);
    }


    public void SetIsStartData(bool _value)
    {
        isStart = _value;
        PlayerPrefs.SetInt("isStart", _value ? 1 : 0);
    }

    public void SetLanguageData(ELanguage _language)
    {
        eLanguage = _language;
        PlayerPrefs.SetInt("language", (int)_language);
    }

    public void LoadData()
    {
        bgmValue = PlayerPrefs.GetFloat("bgmValue", 0.5f);
        effectSoundValue = PlayerPrefs.GetFloat("effectSoundValue", 0.5f);
        gammaValue = PlayerPrefs.GetFloat("gammaValue", 0f);
        sensitivity = PlayerPrefs.GetFloat("sensitivity", 1.0f);
        eLanguage = (ELanguage)PlayerPrefs.GetInt("language", 0);
        isStart = PlayerPrefs.GetInt("isStart", 0) == 1;
    }
}
