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
    private List<RaycastHit> prevHit = new List<RaycastHit>();
    private CameraManager cameraMgr;
    void Start()
    {
        cameraMgr = GameManager.Instance.cameraMgr;
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 screenCenter = new Vector3(cameraMgr.CurCamera.pixelWidth / 2, cameraMgr.CurCamera.pixelHeight / 2);

        ray = cameraMgr.CurCamera.ScreenPointToRay(screenCenter);

        isInteraction = GameManager.Instance.CheckInterection(ray, out RaycastHit _hit, maxDis, interectionLayer);
        

        if (isInteraction)
        {
            prevHit.Add( _hit);
            _hit.transform?.GetComponent<IInteraction>().OnRayHit(selectColor);
            UiManager.Instance.inGameCtr.InGameUiShower.ActivePickUpMark(isInteraction);   
            
            if (GameManager.Instance.inputMgr.IsE)
                _hit.transform.GetComponent<IInteraction>()?.OnInteraction();
        }
        else
        {
            for(int i = 0; i < prevHit.Count; i++)
            {
                prevHit[i].transform?.GetComponent<IInteraction>().OnRayOut();
            }
            prevHit.Clear();
            UiManager.Instance.inGameCtr.InGameUiShower.ActivePickUpMark(isInteraction);
        }

    }


}

    
    

