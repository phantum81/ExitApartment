using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Scriptable Object/Gravity Scriptable ", order = int.MaxValue)]
public class GameEvent : ScriptableObject
{
    private List<EventListener> gravityListener = new List<EventListener>();
    

    public void Raise()
    {
        for (int i = 0; i < gravityListener.Count; i++)
        {
            gravityListener[i].OnEventRaise();
        }
    }
    public void RegisterListener(EventListener _listener)
    {
        gravityListener.Add(_listener);
    }
    public void UnregisterListener(EventListener _listener)
    {
        gravityListener.Remove(_listener);
    }
}
