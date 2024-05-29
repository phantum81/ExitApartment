using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScripts : MonoBehaviour, IInteraction
{

    public ETestFloor etset;
    public void Init()
    {

    }

    public void OnRayHit(Color _color)
    {

    }
    public void OnInteraction(Vector3 _angle)
    {
        switch(etset)
        {
            case ETestFloor.Home:
                for(int i=0; i< GameManager.Instance.unitMgr.floorList.Count; i++)
                {
                    if(i != 0)
                    {
                        GameManager.Instance.unitMgr.floorList[i].SetActive(false);
                    }
                    else
                        GameManager.Instance.unitMgr.floorList[0].SetActive(true);
                }
                
                break;
            case ETestFloor.Monster:
                for (int i = 0; i < GameManager.Instance.unitMgr.floorList.Count; i++)
                {
                    if (i != 1)
                    {
                        GameManager.Instance.unitMgr.floorList[i].SetActive(false);
                    }
                    else
                        GameManager.Instance.unitMgr.floorList[1].SetActive(true);
                }
                break;
            case ETestFloor.None:
                for (int i = 0; i < GameManager.Instance.unitMgr.floorList.Count; i++)
                {
                    if (i != 2)
                    {
                        GameManager.Instance.unitMgr.floorList[i].SetActive(false);
                    }
                    else
                        GameManager.Instance.unitMgr.floorList[2].SetActive(true);
                }
                break;
        }
    }
    public EInteractionType OnGetType()
    {
        return EInteractionType.Press;
    }
    public void OnRayOut()
    {

    }
}
