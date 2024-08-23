using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pumpkin : MonoBehaviour
{
    private EMobType eMobType = EMobType.Pumpkin;

    [Header("보임 판정"),SerializeField]
    private GameObject seePoint;
    public GameObject SeePoint => seePoint;




}
