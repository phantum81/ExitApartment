using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [Header("최대 거리"), SerializeField]
    private float maxDis = 2f;
    private Ray ray;
    private int interectionLayer = 1 << 6;
    
    private bool isInteraction = false;
    private Color selectColor = Color.green;
    private RaycastHit prevHit;
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 screenCenter = new Vector3(Camera.main.pixelWidth / 2, Camera.main.pixelHeight / 2);

        ray = Camera.main.ScreenPointToRay(screenCenter);

        isInteraction = GameManager.Instance.CheckInterection(ray, out RaycastHit _hit, maxDis, interectionLayer);
        

        if (isInteraction)
        {
            prevHit = _hit;
            _hit.transform?.GetComponent<IInteraction>().OnRayHit(selectColor);
            UiManager.Instance.inGameCtr.InGameUiShower.ActivePickUpMark(isInteraction);   
            
            if (Input.GetMouseButtonDown(0))
                _hit.transform.GetComponent<IInteraction>()?.OnInteraction();
        }
        else
        {
            prevHit.transform?.GetComponent<IInteraction>().OnRayOut();
            UiManager.Instance.inGameCtr.InGameUiShower.ActivePickUpMark(isInteraction);
        }

    }

}

    
    

