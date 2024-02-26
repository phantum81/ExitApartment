using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject _go = GameObject.Find("GameManager");
                if (_go == null)
                {
                    _instance = _go.AddComponent<GameManager>();

                }
                if (_instance == null)
                {
                    _instance = _go.GetComponent<GameManager>();
                }
            }
            return _instance;

        }
    }

    public UnitManager unitMgr;
    public CameraManager cameraMgr;
    public InputManager inputMgr;
    public EventManager eventMgr;
    public ItemManager itemMgr;
    

    void Start()
    {
        //GameObject go = new GameObject("CameraMgr");
        //cameraMgr = go.AddComponent<CameraManager>();
        //go.transform.parent = transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool CheckInterection(Ray _ray, out RaycastHit _hit, float _maxDis, int _layer)
    {
        return cameraMgr.CameraCtr.CheckInterection(_ray, out _hit ,_maxDis, _layer);
    }



}
