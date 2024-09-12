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


    public void SetBgmData(float _bgm)
    {
        bgmValue= _bgm;
        
    }
    public void SetEffectData(float _effect)
    {
        effectSoundValue = _effect;
    }

    public void SetGammaData(float _value)
    {
        gammaValue= _value;
    }
    public void SetSensitivityData(float _value)
    {
        sensitivity = _value;
    }
}
