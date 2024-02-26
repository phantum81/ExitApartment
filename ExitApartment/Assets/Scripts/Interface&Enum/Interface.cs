using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState<T>
{
    void OperateEnter(T sender);
    void OperateUdate(T sender);
    void OperateExit(T sender);

}
public interface IContect
{
    void OnContect();


}

public interface IEnemyContect
{
    void OnContect();


}

public interface IInteraction
{

    void OnRayHit(Color _color);
    void OnInteraction();

    void OnRayOut();


}
