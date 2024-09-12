using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    //[Header("플레이어 컨트롤러"), SerializeField]
    //private PlayerController playerCtr;


    Vector3 inputDir;
    public Vector3 InputDir => inputDir;
    Vector3 cameraInputDir;
    public Vector3 CameraInputDir => cameraInputDir;


    private InputBinding inputBind =new InputBinding();

    public InputBinding Inputbind => inputBind;

    private Dictionary<EuserAction, bool> inputDic = new Dictionary<EuserAction, bool>();
    public Dictionary<EuserAction, bool> InputDic => inputDic;

    private void Awake()
    {
        //GameManager.Instance.inputMgr = this;
    }
    // Update is called once per frame
    private void Start()
    {
        Init();
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
        if (inputDic.Count > 0)
            return;
        for(int i = 0; i < inputDic.Count; i++)
        {            
            inputDic.Add((EuserAction)i, false);
        }
       
    }

    public void CheckInputKeys()
    {
        inputDic[EuserAction.MoveForward] = Input.GetKey(inputBind.BindingDic[EuserAction.MoveForward]);
        inputDic[EuserAction.MoveBackward] = Input.GetKey(inputBind.BindingDic[EuserAction.MoveBackward]);
        inputDic[EuserAction.MoveRight] = Input.GetKey(inputBind.BindingDic[EuserAction.MoveRight]);
        inputDic[EuserAction.MoveLeft] = Input.GetKey(inputBind.BindingDic[EuserAction.MoveLeft]);
        inputDic[EuserAction.Run] = Input.GetKey(inputBind.BindingDic[EuserAction.Run]);
        inputDic[EuserAction.Interaction] = Input.GetKeyDown(inputBind.BindingDic[EuserAction.Interaction]);
        inputDic[EuserAction.Throw] = Input.GetKeyDown(inputBind.BindingDic[EuserAction.Throw]);
        inputDic[EuserAction.UseItem] = Input.GetKeyDown(inputBind.BindingDic[EuserAction.UseItem]);
        inputDic[EuserAction.Ui_Menu] = Input.GetKeyDown(inputBind.BindingDic[EuserAction.Ui_Menu]);
        inputDic[EuserAction.Inventory] = Input.GetKeyDown(inputBind.BindingDic[EuserAction.Inventory]);

        inputDic[EuserAction.One] = Input.GetKeyDown(inputBind.BindingDic[EuserAction.One]);
        inputDic[EuserAction.Two] = Input.GetKeyDown(inputBind.BindingDic[EuserAction.Two]);
        inputDic[EuserAction.Three] = Input.GetKeyDown(inputBind.BindingDic[EuserAction.Three]);
        inputDic[EuserAction.Four] = Input.GetKeyDown(inputBind.BindingDic[EuserAction.Four]);
        inputDic[EuserAction.Five] = Input.GetKeyDown(inputBind.BindingDic[EuserAction.Five]);
        inputDic[EuserAction.Six] = Input.GetKeyDown(inputBind.BindingDic[EuserAction.Six]);
        inputDic[EuserAction.Seven] = Input.GetKeyDown(inputBind.BindingDic[EuserAction.Seven]);
        inputDic[EuserAction.Eight] = Input.GetKeyDown(inputBind.BindingDic[EuserAction.Eight]);
        inputDic[EuserAction.Nine] = Input.GetKeyDown(inputBind.BindingDic[EuserAction.Nine]);
    }
}
