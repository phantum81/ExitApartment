using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlopeRay : MonoBehaviour
{

    [Header("레이길이"),SerializeField]
    private float rayDistance;

    private float groundAngle = 0f;
    public float GroundAngle => groundAngle;

    


    private void Update()
    {
        groundAngle = CalculateGroundAngle();
    }
    private float CalculateGroundAngle()
    {

        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hitInfo,
                            rayDistance))
            return Vector3.Angle(Vector3.up, hitInfo.normal);
        return 0f;
    }
}
