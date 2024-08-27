using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TelePortCollider : MonoBehaviour
{
    public UnityEvent onTelePort;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            onTelePort.Invoke();

        }
    }
}
