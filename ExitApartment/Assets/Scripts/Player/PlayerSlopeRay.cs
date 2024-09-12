using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PlayerSlopeRay : MonoBehaviour
{

    [Header("레이길이"),SerializeField]
    private float rayDistance;

    //private float groundAngle = 0f;
    //public float GroundAngle => groundAngle;

    private bool isSlope = false;
    public bool IsSlope => isSlope;

    private LayerMask expectLayer = (1<<7)|(1<<8)|(1<<9);


   


    private void Update()
    {
        //groundAngle = CalculateGroundAngle();
       // isSlope = CheckIsSlope();
        
        
    }
    public float CalculateGroundAngle(Vector3 _inputDir)
    {
        if (_inputDir == Vector3.zero)
            _inputDir = transform.forward;
        if (Physics.Raycast(transform.position, _inputDir, out RaycastHit hitInfo,
                            rayDistance, ~expectLayer))
        {
            
            return Vector3.Angle(Vector3.up, hitInfo.normal);
        }

        return 0f;
    }

    //public bool CheckIsSlope()
    //{
    //    if(groundAngle != 0f)
    //    {
    //        return false;
    //    }
    //    return true;
    //}
}
