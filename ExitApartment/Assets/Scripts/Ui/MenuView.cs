using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    MenuPresent menuPresent;


    void Awake()
    {
        menuPresent = new MenuPresent(this, saveData.data);
        newStartButton.onClick.AddListener(menuPresent.NewStartScene);
        loadButton.onClick.AddListener(menuPresent.LoadScene);
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
}
