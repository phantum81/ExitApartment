using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    [Header("플레이어 컨트롤러"), SerializeField]
    private PlayerController playerCtr;
    [Header("엘리베이터 컨트롤러"), SerializeField]
    private ElevatorController elevatorCtr;
    public ElevatorController ElevatorCtr => elevatorCtr;
    void Start()
    {
        playerCtr.Init();
    }

    // Update is called once per frame
    void Update()
    {
        elevatorCtr.OpenDoor();
    }
}
