using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    private static UiManager _instance;
    public static UiManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject _go = GameObject.Find("UiManager");
                if (_go == null)
                {
                    _instance = _go.AddComponent<UiManager>();

                }
                if (_instance == null)
                {
                    _instance = _go.GetComponent<UiManager>();
                }
            }
            return _instance;

        }
    }

    public InGameUiController inGameCtr;
    
    private void Awake()
    {
        //DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public IEnumerator SetUiInvisible(Transform _target, float _time, float _wait)
    {
        yield return new WaitForSeconds(_wait);
        float curAlpha = 1f; // �ִ� ������ ����
        Color curColor;

        // UI ����� �÷� ������Ʈ�� ������
        Graphic uiGraphic = _target.GetComponent<Image>();
        TextMeshProUGUI uiText = _target.GetComponent<TextMeshProUGUI>();

        if (uiGraphic != null)
        {
            curColor = uiGraphic.color;
        }
        else if (uiText != null)
        {
            curColor = uiText.color;
        }
        else
        {
            // UI ��Ұ� Image�� TextMeshProUGUI ������Ʈ�� ������ ���� ������ �Լ� ����
            yield break;
        }

        // ������ ������ ���̸鼭 UI�� �����ϰ� ����
        while (curAlpha > 0f)
        {
            curAlpha -= Time.deltaTime / _time; // _time ���ȿ� ������ ����
            curColor.a = curAlpha; // �÷��� ���� ä���� ����

            if (uiGraphic != null)
            {
                uiGraphic.color = curColor;
            }
            else if (uiText != null)
            {
                uiText.color = curColor;
            }

            yield return null;
        }

        // ������ 0 �̸����� �������� ���� �����ϱ� ���� 0���� ����
        curColor.a = 1f;

        // �������� UI ����� ������ ������ 0���� ����
        if (uiGraphic != null)
        {
            uiGraphic.color = curColor;
        }
        else if (uiText != null)
        {
            uiText.color = curColor;
        }

        
        _target.gameObject.SetActive(false);

    }

    public IEnumerator SetUiVisible(Transform _target, float _time, float _wait)
    {
        yield return new WaitForSeconds(_wait);
        if(!_target.gameObject.activeSelf )
        {
            _target.gameObject.SetActive(true);
        }
        float curAlpha = 0f; // �ּ� ������ ����
        Color curColor;

        // UI ����� �÷� ������Ʈ�� ������
        Graphic uiGraphic = _target.GetComponent<Image>();
        TextMeshProUGUI uiText = _target.GetComponent<TextMeshProUGUI>();

        if (uiGraphic != null)
        {
            curColor = uiGraphic.color;
        }
        else if (uiText != null)
        {
            curColor = uiText.color;
        }
        else
        {
            // UI ��Ұ� Image�� TextMeshProUGUI ������Ʈ�� ������ ���� ������ �Լ� ����
            yield break;
        }

        // ������ ������ ���̸鼭 UI�� ��Ÿ���� ��
        while (curAlpha < 1f)
        {
            curAlpha += Time.deltaTime / _time; // _time ���ȿ� ������ ����
            curColor.a = curAlpha; // �÷��� ���� ä���� ����

            if (uiGraphic != null)
            {
                uiGraphic.color = curColor;
            }
            else if (uiText != null)
            {
                uiText.color = curColor;
            }

            yield return null;
        }

        // ������ 1�� ���� �ʵ��� 1�� ����
        curColor.a = 1f;

        // �������� UI ����� ������ ������ 1�� ����
        if (uiGraphic != null)
        {
            uiGraphic.color = curColor;
        }
        else if (uiText != null)
        {
            uiText.color = curColor;
        }

        _target.gameObject.SetActive(true);
    }



}
