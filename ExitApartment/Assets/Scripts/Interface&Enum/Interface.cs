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

    void OnExit();
}

public interface IGravityChange
{
    void OnGravityChange();
}

public interface IEventContect
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
}
