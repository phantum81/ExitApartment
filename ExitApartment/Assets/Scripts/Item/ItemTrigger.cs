using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTrigger : MonoBehaviour, IInteraction
{
    private Rigidbody rigd;
    public void Init()
    {
        rigd = transform.GetComponentInChildren<Rigidbody>();
    }


    public void OnRayHit(Color _color)
    {
        

    }
    public void OnInteraction(Vector3 _angle)
    {
        if (rigd.isKinematic)
            rigd.isKinematic = false;
        gameObject.layer = 0;
        

    }
    public EInteractionType OnGetType()
    {
        return EInteractionType.Find;
    }
    public void OnRayOut()
    {
        
    }
}
