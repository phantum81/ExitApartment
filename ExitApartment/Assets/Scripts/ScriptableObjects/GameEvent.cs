using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Scriptable Object/Gravity Scriptable ", order = int.MaxValue)]
public class GameEvent : ScriptableObject
{
    public List<EventListener> gravityListener = new List<EventListener>();
    public List<EventListener> die12FListener = new List<EventListener>();

    public void GravityRaise()
    {
        for (int i = gravityListener.Count - 1; i >= 0; i--)
        {
            gravityListener[i].OnEventRaise();
        }
    }
    public void GravityRegisterListener(EventListener _listener)
    {
        gravityListener.Add(_listener);
    }
    public void GravityUnregisterListener(EventListener _listener)
    {
        gravityListener.Remove(_listener);
    }




    public void Die12FRaise()
    {
        for (int i = die12FListener.Count-1; i >= 0; i--)
        {
            die12FListener[i].OnEventRaise();
        }
    }
    public void Die12FRegisterListener(EventListener _listener)
    {
        die12FListener.Add(_listener);
    }
    public void Die12FUnregisterListener(EventListener _listener)
    {
        die12FListener.Remove(_listener);
    }
}
