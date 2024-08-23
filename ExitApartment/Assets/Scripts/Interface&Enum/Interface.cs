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
