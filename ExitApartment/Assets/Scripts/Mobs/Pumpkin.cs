using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pumpkin : MonoBehaviour, IEnemyContect
{
    private EMobType eMobType = EMobType.Pumpkin;

    [Header("���� ����"),SerializeField]
    private GameObject seePoint;
    public GameObject SeePoint => seePoint;

    public void OnContect()
    {

    }


}
