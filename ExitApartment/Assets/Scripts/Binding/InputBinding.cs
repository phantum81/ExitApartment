using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class InputBinding
{
    private Dictionary<EuserAction, KeyCode> bindingDic;
    public Dictionary<EuserAction, KeyCode> BindingDic => bindingDic;

    public InputBinding()
    {
        bindingDic = new Dictionary<EuserAction, KeyCode>();

        
        ResetAll();
    }

    public InputBinding( SerializableInputBinding _sib)
    {
        bindingDic = new Dictionary<EuserAction, KeyCode>();
        foreach(var pair in _sib.bindPairs)
        {
            bindingDic[pair.key] = pair.value;
        }
    }


    //public void ApplyNewBindings(InputBinding _newBinding)
    //{
    //    bindingDic = new Dictionary<EuserAction, KeyCode>(_newBinding.bindingDic);
    //}

    public void Bind(in EuserAction _action, in KeyCode _key, bool _allowOverlap = false)
    {
        if(!_allowOverlap && bindingDic.ContainsValue(_key))
        {
            Dictionary<EuserAction, KeyCode> copy = new Dictionary<EuserAction, KeyCode>(bindingDic);
            foreach(var pair in copy)
            {
                if(pair.Value.Equals(_key))
                {
                    bindingDic[pair.Key] = KeyCode.None;

                    
                }
            }
        }
        bindingDic[_action] = _key;
    }

    public void ResetAll()
    {
        Bind(EuserAction.MoveForward, KeyCode.W);
        Bind(EuserAction.MoveBackward, KeyCode.S);
        Bind(EuserAction.MoveRight, KeyCode.D);
        Bind(EuserAction.MoveLeft, KeyCode.A);
        Bind(EuserAction.Run, KeyCode.LeftShift);
        Bind(EuserAction.Interaction, KeyCode.E);
        Bind(EuserAction.Throw, KeyCode.G);
        Bind(EuserAction.UseItem, KeyCode.F);
        Bind(EuserAction.Ui_Menu, KeyCode.Escape);


        Bind(EuserAction.One, KeyCode.Alpha1);
        Bind(EuserAction.Two, KeyCode.Alpha2);
        Bind(EuserAction.Three, KeyCode.Alpha3);
        Bind(EuserAction.Four, KeyCode.Alpha4);
        Bind(EuserAction.Five, KeyCode.Alpha5);
        Bind(EuserAction.Six, KeyCode.Alpha6);
        Bind(EuserAction.Seven, KeyCode.Alpha7);
        Bind(EuserAction.Eight, KeyCode.Alpha8);
        Bind(EuserAction.Nine, KeyCode.Alpha9);





    }




    public void SaveFile()
    {
        SerializableInputBinding sib = new SerializableInputBinding(this);
        string js = JsonUtility.ToJson(sib);

        
    }
}
