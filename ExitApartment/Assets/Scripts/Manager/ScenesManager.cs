using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    [SerializeField]
    private GameData data;

    private GameObject menu;
    public GameObject Menu => menu;

    public delegate void MenuSceneUnload();
    public MenuSceneUnload onSceneUnload;

     

    private void Awake()
    {
        
        GameManager.Instance.sceneMgr = this;
    }
    private void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnEnable()
    {
        // ���� �ε�� ������ ȣ��Ǵ� �̺�Ʈ ���
        SceneManager.sceneLoaded += OnSceneLoaded;
        // ���� ��ε�Ǳ� ������ ȣ��
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    private void OnDisable()
    {
        // �� �ε� �̺�Ʈ ����
        SceneManager.sceneLoaded -= OnSceneLoaded;
        // ���� ��ε�Ǳ� ������ ȣ��
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }



    private void OnSceneLoaded(Scene _scene, LoadSceneMode _mode)
    {
        if (SceneManager.GetActiveScene().name == "InGameScene")
        {
            // Ư�� ���� ��� ȣ���� �Լ��� �����մϴ�.
            GameManager.Instance.Init();
            GameManager.Instance.SetGameState(EgameState.InGame);
        }
        if(SceneManager.GetActiveScene().name == "MenuScene")
        {
            Time.timeScale = 1f;
            AudioListener.pause = false;
            
            GameManager.Instance.SetGameState(EgameState.Menu);
            if (GameManager.Instance.eFloorType == EFloorType.Looby)
            {
                data.ResetData();
            }
               
        }


    }


    private void OnSceneUnloaded(Scene _current)
    {
        if (SceneManager.GetActiveScene().name == "InGameScene")
        {

        }
        if (SceneManager.GetActiveScene().name == "MenuScene")
        {
            //onSceneUnload();
        }
    }

    public void SetMenuObject(GameObject _menu, Transform _parent)
    {
        menu = _menu;
        _parent = menu.transform.parent;
    }
    public GameObject GetMenuObject()
    {
        return menu;
    }

}
