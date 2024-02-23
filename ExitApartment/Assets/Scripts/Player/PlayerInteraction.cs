using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [Header("최대 거리"),SerializeField]
    private float maxDis = 2f;
    private Ray ray;
    private int interectionLayer = 1<<6;

    void Start()
    {
        Vector3 screenCenter = new Vector3(Camera.main.pixelWidth / 2, Camera.main.pixelHeight / 2);
        ray = Camera.main.ScreenPointToRay(screenCenter);
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.Checkinterection(ray, maxDis, interectionLayer))
        {

        }
        else
        {

        }
        
    }
    
}
