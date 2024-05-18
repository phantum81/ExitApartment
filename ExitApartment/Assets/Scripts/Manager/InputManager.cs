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


    private InputBinding inputbind =new InputBinding();

    public InputBinding Inputbind => inputbind;

    private Dictionary<EuserAction, bool> inputDic = new Dictionary<EuserAction, bool>();
    public Dictionary<EuserAction, bool> InputDic => inputDic;

    // Update is called once per frame
    private void Start()
    {
        CheckInputKeys();
    }
    void Update()
    {
        CheckInputKeys();
        GetInputDir();
        GetCameraInput();
    }

    private void GetInputDir()
    {

        float x = 0f;
        float z = 0f;

        x = (inputDic[EuserAction.MoveRight] ? 1f : 0f) - (inputDic[EuserAction.MoveLeft] ? 1f : 0f);
        z = (inputDic[EuserAction.MoveForward] ? 1f : 0f) - (inputDic[EuserAction.MoveBackward] ? 1f : 0f);

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
        inputDic[EuserAction.Run] = Input.GetKey(inputbind.BindingDic[EuserAction.Run]);
        inputDic[EuserAction.Interaction] = Input.GetKeyDown(inputbind.BindingDic[EuserAction.Interaction]);
        inputDic[EuserAction.Throw] = Input.GetKeyDown(inputbind.BindingDic[EuserAction.Throw]);
        inputDic[EuserAction.UseItem] = Input.GetKeyDown(inputbind.BindingDic[EuserAction.UseItem]);
        inputDic[EuserAction.Ui_Menu] = Input.GetKeyDown(inputbind.BindingDic[EuserAction.Ui_Menu]);

        inputDic[EuserAction.One] = Input.GetKeyDown(inputbind.BindingDic[EuserAction.One]);
        inputDic[EuserAction.Two] = Input.GetKeyDown(inputbind.BindingDic[EuserAction.Two]);
        inputDic[EuserAction.Three] = Input.GetKeyDown(inputbind.BindingDic[EuserAction.Three]);
        inputDic[EuserAction.Four] = Input.GetKeyDown(inputbind.BindingDic[EuserAction.Four]);
        inputDic[EuserAction.Five] = Input.GetKeyDown(inputbind.BindingDic[EuserAction.Five]);
        inputDic[EuserAction.Six] = Input.GetKeyDown(inputbind.BindingDic[EuserAction.Six]);
        inputDic[EuserAction.Seven] = Input.GetKeyDown(inputbind.BindingDic[EuserAction.Seven]);
        inputDic[EuserAction.Eight] = Input.GetKeyDown(inputbind.BindingDic[EuserAction.Eight]);
        inputDic[EuserAction.Nine] = Input.GetKeyDown(inputbind.BindingDic[EuserAction.Nine]);
    }
}
