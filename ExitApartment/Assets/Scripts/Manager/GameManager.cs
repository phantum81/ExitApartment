using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;

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
    public SoundManager soundMgr;
    

    public Action onGetForestHumanity;

    private int humanityScore = 0;
    public int HumanityScore => humanityScore;

    
    void Start()
    {
        onGetForestHumanity += AddHumanityScore;
        itemMgr.Init();
        inputMgr.Init();
    }

    
    void Update()
    {

    }

    public bool CheckInterection(Ray _ray, out RaycastHit _hit, float _maxDis, int _layer)
    {
        return cameraMgr.CameraCtr.CheckInterection(_ray, out _hit ,_maxDis, _layer);
    }

    public void ZoomCamera( Camera _zoomCam, Camera _mainCam, Transform _target, float _distance)
    {
        cameraMgr.ZoomCamera.ZoomCamera( _zoomCam, _mainCam, _target, _distance);
    }

    public void ZoomMove(Camera _zoomCam, Collider _col, float _dis)
    {
        cameraMgr.ZoomCamera.StartCoroutine(cameraMgr.ZoomCamera.ZoomMove(_zoomCam, _col, _dis));
    }

    
    public void AddHumanityScore()
    {
        humanityScore++;
    }
    public int GetHumanityScore()
    {
        return humanityScore;
    }
}
