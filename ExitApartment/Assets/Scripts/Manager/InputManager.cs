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

    private bool isG = false;
    public bool IsG => isG;


    private InputBinding inputbind =new InputBinding();

    public InputBinding Inputbind => inputbind;

    private Dictionary<EuserAction, bool> inputDic = new Dictionary<EuserAction, bool>();
    public Dictionary<EuserAction, bool> InputDic => inputDic;

    // Update is called once per frame
    void Update()
    {
        CheckInputKeys();
        GetInputDir();
        GetCameraInput();
    }

    private void GetInputDir()
    {

        float x=0f;
        float z=0f;

        if (inputDic[EuserAction.MoveForward])
        {
            z = 1f; 
        }
        else if (inputDic[EuserAction.MoveBackward])
        {
            z = -1f;
        }

        if (inputDic[EuserAction.MoveRight])
        {
            x = 1f; 
        }
        else if (inputDic[EuserAction.MoveLeft])
        {
            x = -1f;
        }

        inputDir = new Vector3(x, 0, z);

    }
    private void GetCameraInput()
    {
        float x = -Input.GetAxis("Mouse Y");
        float y = Input.GetAxis("Mouse X");
        
        cameraInputDir = new Vector3(x, y,0);
    }

    public void Init()
    {

        for(int i = 0; i < inputDic.Count; i++)
        {            
            inputDic.Add((EuserAction)i, false);
        }
       
    }

    public void CheckInputKeys()
    {
        inputDic[EuserAction.MoveForward] = Input.GetKey(inputbind.BindingDic[EuserAction.MoveForward]);
        inputDic[EuserAction.MoveBackward] = Input.GetKey(inputbind.BindingDic[EuserAction.MoveBackward]);
        inputDic[EuserAction.MoveRight] = Input.GetKey(inputbind.BindingDic[EuserAction.MoveRight]);
        inputDic[EuserAction.MoveLeft] = Input.GetKey(inputbind.BindingDic[EuserAction.MoveLeft]);
        inputDic[EuserAction.Run] = Input.GetKeyDown(inputbind.BindingDic[EuserAction.Run]);
        inputDic[EuserAction.Interaction] = Input.GetKeyDown(inputbind.BindingDic[EuserAction.Interaction]);
        inputDic[EuserAction.Throw] = Input.GetKeyDown(inputbind.BindingDic[EuserAction.Throw]);
        inputDic[EuserAction.UseItem] = Input.GetKeyDown(inputbind.BindingDic[EuserAction.UseItem]);
        inputDic[EuserAction.Ui_Menu] = Input.GetKeyDown(inputbind.BindingDic[EuserAction.Ui_Menu]);
        
    }
}
