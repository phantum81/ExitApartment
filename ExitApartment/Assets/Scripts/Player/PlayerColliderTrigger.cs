using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColliderTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        
        IEventContect col = other.GetComponent<IEventContect>();
        
        col?.OnContect(ESOEventType.OnGravity);
    }

}
