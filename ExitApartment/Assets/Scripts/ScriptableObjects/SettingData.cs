using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SettingData", menuName = "ScriptableData/SettingData")]
public class SettingData : MonoBehaviour
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
}
