using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }




}
