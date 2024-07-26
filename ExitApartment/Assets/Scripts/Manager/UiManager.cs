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
        float curAlpha = 1f; // 최대 투명도로 시작
        Color curColor;

        // UI 요소의 컬러 컴포넌트를 가져옴
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
            // UI 요소가 Image나 TextMeshProUGUI 컴포넌트를 가지고 있지 않으면 함수 종료
            yield break;
        }

        // 투명도를 서서히 줄이면서 UI를 투명하게 만듦
        while (curAlpha > 0f)
        {
            curAlpha -= Time.deltaTime / _time; // _time 동안에 투명도를 줄임
            curColor.a = curAlpha; // 컬러의 알파 채널을 갱신

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

        // 투명도가 0 미만으로 내려가는 것을 방지하기 위해 0으로 설정
        curColor.a = 1f;

        // 마지막에 UI 요소의 투명도를 완전히 0으로 설정
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
        float curAlpha = 0f; // 최소 투명도로 시작
        Color curColor;

        // UI 요소의 컬러 컴포넌트를 가져옴
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
            // UI 요소가 Image나 TextMeshProUGUI 컴포넌트를 가지고 있지 않으면 함수 종료
            yield break;
        }

        // 투명도를 서서히 높이면서 UI를 나타나게 함
        while (curAlpha < 1f)
        {
            curAlpha += Time.deltaTime / _time; // _time 동안에 투명도를 높임
            curColor.a = curAlpha; // 컬러의 알파 채널을 갱신

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

        // 투명도가 1을 넘지 않도록 1로 설정
        curColor.a = 1f;

        // 마지막에 UI 요소의 투명도를 완전히 1로 설정
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
