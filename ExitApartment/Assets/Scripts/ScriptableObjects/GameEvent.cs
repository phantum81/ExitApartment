using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Scriptable Object/Gravity Scriptable ", order = int.MaxValue)]
public class GameEvent : ScriptableObject
{
    public List<EventListener> gravityListener = new List<EventListener>();
    public List<EventListener> die12FListener = new List<EventListener>();
    public List<EventListener> alive12FListener = new List<EventListener>();
    public List<EventListener> magicStoneListener = new List<EventListener>();
    public List<EventListener> Clear12FListener = new List<EventListener>();
    public void magicStoneRaise()
    {
        for (int i = magicStoneListener.Count - 1; i >= 0; i--)
        {
            magicStoneListener[i].OnEventRaise();
        }
    }
    public void magicStoneRegisterListener(EventListener _listener)
    {
        magicStoneListener.Add(_listener);
    }
    public void magicStoneUnregisterListener(EventListener _listener)
    {
        magicStoneListener.Remove(_listener);
    }



    public void Clear12FRaise()
    {
        for (int i = magicStoneListener.Count - 1; i >= 0; i--)
        {
            Clear12FListener[i].OnEventRaise();
        }
    }
    public void Clear12FRegisterListener(EventListener _listener)
    {
        Clear12FListener.Add(_listener);
    }
    public void Clear12FUnregisterListener(EventListener _listener)
    {
        Clear12FListener.Remove(_listener);
    }

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


    public void Alive12FRaise()
    {
        for (int i = alive12FListener.Count - 1; i >= 0; i--)
        {
            alive12FListener[i].OnEventRaise();
        }
    }
    public void Alive12FRegisterListener(EventListener _listener)
    {
        alive12FListener.Add(_listener);
    }
    public void Alive12FUnregisterListener(EventListener _listener)
    {
        alive12FListener.Remove(_listener);
    }
}
