using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class OptionPresent
{

    IOptionMenuView optionMenu;
    SettingData settingData;

    public OptionPresent( SettingData _data, IOptionMenuView _optionMenu)
    {
        optionMenu = _optionMenu;
        settingData = _data;
    }
    #region 사운드
    public void SetBgm(float _value)
    {
        optionMenu.SetBgmVolume(_value);

    }
    public void SetEffect(float _value)
    {
        optionMenu.SetEffectVolume(_value);

    }

    public void SaveSoundVolume()
    {
        (float bgm, float effect) = optionMenu.GetSoundVolume();
        settingData.SetBgmData(bgm);
        settingData.SetEffectData(effect);
        optionMenu.CloseParentPanel();

    }

    public void NoSaveSoundVolume()
    {
        optionMenu.SetSoundVolume(settingData.BgmValue, settingData.EffectSoundValue);
        optionMenu.CloseParentPanel();
    }
    #endregion

    #region 감마
    public void SetGamma(float _value)
    {
        optionMenu.SetGammaValue(_value);

    }

    public void SaveGamma()
    {
        float value = optionMenu.GetGammaValue();
        if (value == -1) return;

        settingData.SetGammaData(value);
        optionMenu.CloseParentPanel();
    }

    public void NoSaveGamma()
    {
        optionMenu.SetGammaValue(settingData.GammaValue);
        optionMenu.SetGammaSliderValue(settingData.GammaValue);
        optionMenu.CloseParentPanel();
    }
    #endregion

    #region 입력

    public void SetSensitivity(float _value)
    {
        optionMenu.SetSensitivityValue(_value);
        
    }

    public void SaveInput()
    {
        float value = optionMenu.GetSensitivityValue();
        settingData.SetSensitivityData(value);
        optionMenu.SaveInput();
        optionMenu.CloseParentPanel();
    }

    public void NoSaveInput()
    {
        optionMenu.SetInputValue(settingData.Sensitivity);        
        optionMenu.NoSaveInput();
        optionMenu.CloseParentPanel();

    }
    #endregion


    #region 언어
    public void SetLanguage()
    {
        optionMenu.SetLanguage();

    }

    #endregion


    public void Init()
    {
        settingData.LoadData();
        optionMenu.LoadData(settingData.BgmValue, settingData.EffectSoundValue, settingData.GammaValue, settingData.Sensitivity);
        optionMenu.StartRecommandPanel(settingData.IsStart);
        settingData.SetIsStartData(true);

    }
    
    // 저장 데이터로 처음킨건지 아닌건지 확인하고 끌때는 리셋시키고 (혹은 저장데이터에 포함을 안시키고) 실행시키기
}
