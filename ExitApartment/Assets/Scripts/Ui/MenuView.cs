using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
