using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuView : MonoBehaviour, IMenuView
{
    [Header("���ν��� ��ư")]
    public Button newStartButton;
    [Header("�̾��ϱ� ��ư")]
    public Button loadButton;
    [Header("�ɼ� ��ư")]
    public Button optionButton;
    [Header("�ɼ� �ǳ�")]
    public GameObject optionPanel;
    [Header("���̺굥����"),SerializeField]
    private SaveData saveData;
    [Header("�� ���� â"), SerializeField]
    private GameObject sceneChangePanel;
    MenuPresent menuPresent;
    private UiManager uiMgr;
    

    void Awake()
    {
        menuPresent = new MenuPresent(this, saveData.data);
        uiMgr = UiManager.Instance;
        newStartButton.onClick.AddListener(menuPresent.NewStartScene);
        loadButton.onClick.AddListener(menuPresent.LoadDataScene);
        optionButton.onClick.AddListener(menuPresent.OpenOption);
        
        
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowOptionPanel()
    {
        optionPanel.SetActive(true);
    }

    public void LoadInGameScene()
    {
        StartCoroutine(LoadSceneCorouutine());
    }
    private IEnumerator LoadSceneCorouutine()
    {
        yield return StartCoroutine(uiMgr.SetUiVisible(sceneChangePanel.transform, 1f, 0f));
        SceneManager.LoadScene("InGameScene");
    }
    
}
