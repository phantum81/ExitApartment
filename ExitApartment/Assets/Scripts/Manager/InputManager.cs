using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [Header("플레이어 컨트롤러"), SerializeField]
    private PlayerController playerCtr;


    Vector3 inputDir;
    public Vector3 InputDir => inputDir;
    Vector3 cameraInputDir;
    public Vector3 CameraInputDir => cameraInputDir;

    
    private bool isShift = false;
    public bool IsShift => isShift;
    private bool isE = false;
    public bool IsE => isE;

    private bool isF = false;
    public bool IsF => isF;
    // Update is called once per frame
    void Update()
    {
        GetInputDir();
        GetCameraInput();
        isShift = Input.GetKey(KeyCode.LeftShift);
        isE = Input.GetKeyDown(KeyCode.E);
        isF = Input.GetKeyDown(KeyCode.F);
    }

    private void GetInputDir()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        inputDir = new Vector3(x, 0, z);

    }
    private void GetCameraInput()
    {
        float x = -Input.GetAxis("Mouse Y");
        float y = Input.GetAxis("Mouse X");
        
        cameraInputDir = new Vector3(x, y,0);
    }
}
