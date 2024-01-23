using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GravityDieCollider : MonoBehaviour, IContect
{
    [SerializeField]
    private float shake = 1f;
    [SerializeField]
    private float amount = 1f;
    public void OnContect()
    {
        GameManager.Instance.eventMgr.OnDead12F();
        
    }
}
