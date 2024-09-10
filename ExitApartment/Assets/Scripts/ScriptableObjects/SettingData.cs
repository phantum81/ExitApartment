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

}
