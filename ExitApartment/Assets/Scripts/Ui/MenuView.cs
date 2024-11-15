using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuView : MonoBehaviour, IMenuView
{
    [Header("새로시작 버튼")]
    public Button newStartButton;
    [Header("이어하기 버튼")]
    public Button loadButton;
    [Header("옵션 버튼")]
    public Button optionButton;
    [Header("옵션 판넬")]
    public GameObject optionPanel;
    [Header("세이브데이터"),SerializeField]
    private SaveData saveData;
    [Header("씬 변경 창"), SerializeField]
    private GameObject sceneChangePanel;

    [Header("종료버튼"), SerializeField]
    private Button exitButton;

    [Header("동의 버튼"), SerializeField]
    private Button applyButton;
    [Header("취소 버튼"), SerializeField]
    private Button cancelButton;
    [Header("취소 버튼"), SerializeField]
    private GameObject warningPan;

    MenuPresent menuPresent;
    private UiManager uiMgr;
    

    void Awake()
    {
        menuPresent = new MenuPresent(this, saveData.data);
        uiMgr = UiManager.Instance;
        newStartButton.onClick.AddListener(menuPresent.NewStartScene);
        loadButton.onClick.AddListener(menuPresent.LoadDataScene);
        optionButton.onClick.AddListener(menuPresent.OpenOption);
        exitButton.onClick.AddListener(menuPresent.ClickExit);
        
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
    private void ExitGame()
    {
        Application.Quit();
    }
    private void Cancel()
    {
        warningPan.SetActive(false);
    }
    public void ClickExitButton()
    {
        warningPan.SetActive(true);
        applyButton.onClick.AddListener(ExitGame);
        cancelButton.onClick.AddListener(Cancel);
    }
    
    
}
