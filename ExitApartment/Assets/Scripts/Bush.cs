using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Bush : MonoBehaviour, IContect
{
    public UnityEvent onLongGrass;
    public UnityEvent onReturn;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnContect()
    {
        onLongGrass?.Invoke();
    }

    public void OnExit()
    {
        onReturn?.Invoke();
    }
}
