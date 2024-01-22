using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    [Header("�÷��̾� ��Ʈ�ѷ�"), SerializeField]
    private PlayerController playerCtr;
    public PlayerController PlayerCtr=>playerCtr;
    [Header("���������� ��Ʈ�ѷ�"), SerializeField]
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


    public void ChangeGravity(Rigidbody _rigd, Vector3 _gravity)
    {
        _rigd.useGravity = false;
        _rigd.AddForce(_gravity * 9.81f * _rigd.mass, ForceMode.Acceleration);

    }



}
