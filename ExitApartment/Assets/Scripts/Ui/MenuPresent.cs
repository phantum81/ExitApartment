using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPresent
{
    private IMenuView menuView;
    private MenuData data;
    
    public MenuPresent(IMenuView _menuView, GameData _data)
    {
        menuView = _menuView;
        data = new MenuData(_data);
        
    }

    public void NewStartScene()
    {
        data.DataReset();
        menuView.LoadInGameScene();
        
    
    }
    public void LoadDataScene()
    {
        menuView.LoadInGameScene();
        
    }
    public void OpenOption()
    {
        
        menuView.ShowOptionPanel();
    }
}
