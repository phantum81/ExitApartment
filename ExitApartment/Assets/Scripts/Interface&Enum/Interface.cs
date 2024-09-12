using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState<T>
{
    void OperateEnter(T sender);
    void OperateUpdate(T sender);
    void OperateExit(T sender);

}
public interface IContect
{

    void OnContect();

    void OnStay();
    void OnExit();
}

public interface IGravityChange
{
    void OnGravityChange();
}

public interface ISOEventContect
{

    void OnContect(ESOEventType _type);


}

public interface IEnemyContect
{
    void OnContect();


}

public interface IUseItem
{
    void OnUseItem();

    void OnThrowItem();
}

public interface IInteraction
{

    void Init();

    void OnRayHit(Color _color);
    void OnInteraction(Vector3 _angle);

    void OnRayOut();

    EInteractionType OnGetType();


}
public interface IMenuView
{
    void ShowOptionPanel();

    void LoadInGameScene();
}

public interface IInGameMenuView
{

    void LoadMenuClick();

    void CloseGameClick();

    void ShowOptionPanel();
}

public interface IOptionMenuView
{
    public void LoadData(float _bgm, float _effect, float _gamma, float _sensitivity);
    public void SetBgmVolume(float _value);

    public void SetEffectVolume(float _value);

    /// <summary>
    /// 첫번쨰 bgm 두번째 effect
    /// </summary>
    /// <returns></returns>
    public (float, float) GetSoundVolume();

    public void SetSoundVolume(float _bgm, float _effecet);


    public void SetGammaValue(float _value);

    public float GetGammaValue();

    public void SetGammaSliderValue(float _value);

    public void SetSensitivityValue(float _value);

    public void SetInputValue(float _value);


    public float GetSensitivityValue();


    public void CloseParentPanel();

    //public void ShowSoundOption();
    //public void ShowKeySettingOption();

    //public void ShowBrightOption();
}
