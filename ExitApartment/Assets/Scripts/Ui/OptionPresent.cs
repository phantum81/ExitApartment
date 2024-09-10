using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionPresent
{
    IOptionMenuView optionView;
    OptionData optiondata;
    public OptionPresent(IOptionMenuView _optionView, SettingData _data)
    {
        optionView = _optionView;
        optiondata = new OptionData(_data);
    }
}
