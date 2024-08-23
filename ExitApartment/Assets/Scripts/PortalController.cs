using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.XR;
using static UnityEngine.GraphicsBuffer;

public class PortalController : MonoBehaviour
{
    private Transform playerCamera;  // 플레이어의 카메라
    public Transform portal;
    public Transform otherPortal;
    private Transform portalCamera;  // 포탈 너머를 바라보는 카메라 트랜스폼

    public RenderTexture renderTexture;


    public float viewValue = 1.2f;

    Quaternion portalCameraInitialRotation;
    Quaternion playerCameraInitialRotation;

    private void Start()
    {
        playerCamera = Camera.main.transform;
        portalCamera = this.transform;
        
        renderTexture.width = Screen.width;
        renderTexture.height = Screen.height;


        portalCameraInitialRotation = portalCamera.rotation;
        playerCameraInitialRotation = playerCamera.rotation;

    }
    void Update()
    {

        // Calculate the rotation offset
        Quaternion localRotation = Quaternion.Inverse(portal.rotation) * playerCamera.rotation;

        // Apply the rotation to portalCam, considering land2's rotation
        portalCamera.rotation = otherPortal.rotation * localRotation;



        //Calculate the offset from land1 to playerCam in local space
        Vector3 localOffset = portal.InverseTransformPoint(playerCamera.position);

        // Apply this offset to land2 to position the portalCam
        Vector3 globalOffset = otherPortal.TransformPoint(localOffset);

        portalCamera.position = globalOffset;

    }
}
