using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pumpkin : MonoBehaviour, IEnemyContect
{
    private EMobType eMobType = EMobType.Pumpkin;

    [Header("���� ����"),SerializeField]
    private GameObject seePoint;
    public GameObject SeePoint => seePoint;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnContect()
    {

    }


}
