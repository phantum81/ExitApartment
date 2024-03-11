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
    private CameraManager cameraMgr;
    private PlayerController playerCtr;
    private InputManager inputMgr;
    private RaycastHit preHit;



    void Start()
    {
        cameraMgr = GameManager.Instance.cameraMgr;
        playerCtr = gameObject.GetComponent<PlayerController>();
        inputMgr = GameManager.Instance.inputMgr;
    }

    // Update is called once per frame
    void Update()
    {

        ray = CenterRay();

        isInteraction = GameManager.Instance.CheckInterection(ray, out RaycastHit _hit, maxDis, interectionLayer);


        if (isInteraction)
        {            
            OnInteraction(_hit);
        }
        else
        {
            OffInteraction();
        }


        if(playerCtr.CurItem != null)
        {
            if (inputMgr.InputDic[EuserAction.UseItem])
            {
                playerCtr.CurItem.GetComponent<IUseItem>().OnUseItem();
            }
            if (inputMgr.InputDic[EuserAction.Throw])
            {
                playerCtr.CurItem.GetComponent<IUseItem>().OnThrowItem();
            }
        }

    }

    private Ray CenterRay()
    {
        
        Vector3 screenCenter = new Vector3(cameraMgr.CurCamera.pixelWidth / 2, cameraMgr.CurCamera.pixelHeight / 2);

        ray = cameraMgr.CurCamera.ScreenPointToRay(screenCenter);
        return ray;

    }

    private void OnInteraction(RaycastHit _hit)
    {
        if (preHit.collider == null)
            preHit = _hit;


        preHit.transform?.GetComponent<IInteraction>().OnRayHit(selectColor);
        UiManager.Instance.inGameCtr.InGameUiShower.ActivePickUpMark(isInteraction);

        if (inputMgr.InputDic[EuserAction.Interaction])
            preHit.transform.GetComponent<IInteraction>()?.OnInteraction(Vector3.zero);
    }

    private void OffInteraction()
    {
        preHit.transform?.GetComponent<IInteraction>()?.OnRayOut();
        preHit = new RaycastHit();

        UiManager.Instance.inGameCtr.InGameUiShower.ActivePickUpMark(isInteraction);
    }

}

    
    

