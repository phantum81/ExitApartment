using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorPan : MonoBehaviour, IInteraction
{
    private Color originColor;
    private Material curMaterial;


    private void Start()
    {
        curMaterial = transform.GetComponent<Renderer>().material;
        originColor = curMaterial.color;
    }
    public void OnRayHit( Color _color)
    {
        curMaterial.color = _color;

    }
    public void OnInteraction()
    {

    }
    public void OnRayOut()
    {
        curMaterial.color = originColor;
    }
}
