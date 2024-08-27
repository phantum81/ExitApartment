using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pumpkin : MonoBehaviour
{
    private EMobType eMobType = EMobType.Pumpkin;

    [Header("���� ����"),SerializeField]
    private GameObject seePoint;
    UnitManager unitMgr;

    private void Start()
    {
        unitMgr = GameManager.Instance.unitMgr;
        unitMgr.SeePointsDic.Add(ESeePoint.Pumpkin, seePoint.transform);
        unitMgr.ShowObject(transform, false);
    }

}
