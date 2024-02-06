using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/SODie12F Scriptable ")]
public class Die12FEvent : ScriptableObject
{
    public List<EventListener> Die12FListener = new List<EventListener>();


    public void Raise()
    {
        for (int i = 0; i < Die12FListener.Count; i++)
        {
            Die12FListener[i].OnEventRaise();
        }
    }
    public void RegisterListener(EventListener _listener)
    {
        Die12FListener.Add(_listener);
    }
    public void UnregisterListener(EventListener _listener)
    {
        Die12FListener.Remove(_listener);
    }
}
