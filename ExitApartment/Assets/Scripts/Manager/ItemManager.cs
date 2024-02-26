using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    private Dictionary<ElevatorPan, ElevatorNumData> elevatorFloorDic;
    public Dictionary<ElevatorPan, ElevatorNumData> ElevatorFloorDic => elevatorFloorDic;
    [SerializeField]
    private List<ElevatorPan> elevatorFloorList;

    
    void Start()
    {

        for(int i = 0; i < elevatorFloorList.Count; i++)
        {
            elevatorFloorDic.Add(elevatorFloorList[i], new ElevatorNumData(elevatorFloorList[i].GetComponentInChildren<TextMeshPro>().text));

        }
    }

    
    void Update()
    {
        Debug.Log("¿¨");
    }



}
