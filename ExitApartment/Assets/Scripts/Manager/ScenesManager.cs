using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    [SerializeField]
    private GameData data;
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
    }

    private void OnDisable()
    {
        // �� �ε� �̺�Ʈ ����
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
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
            if (GameManager.Instance.eFloorType == EFloorType.Looby)
            {
                data.ResetData();
            }
               
        }


    }



}
