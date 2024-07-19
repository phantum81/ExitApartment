using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class OnDeadCameraController : MonoBehaviour
{
    
    private WaitForSeconds lookMonsterWait;
    [Header("회전기다림"), SerializeField]
    private float lookMonster_time=2f;
    private float validAngle = 0.4f;


    [Header("회전속도"), SerializeField]
    private float rotateSpeed = 4f;
    [Header("최소 회전속도"), SerializeField]
    private float min_rotateSpeed = 3f;
    [Header("최대 회전속도"), SerializeField]
    private float max_rotateSpeed = 8f;

    [Header("상하 움직임 속도"), SerializeField]
    private float _d_speed = 12f;
    public float  D_speed => _d_speed;
    [Header("상하 움직임 정도"), SerializeField]
    private float _d_shake = 0.0008f;
    public float D_shake => _d_shake;



    private Vector3 shakeDir = new Vector3(7f, 30f, 7f);
    

    private CameraController cameraCtr;
    private CameraManager cameraMgr;
    private PlayableDirector playableDir;
    [Header("죽음 캠"), SerializeField]
    private TimelineAsset deadAsset;
    private Collider deadCollider;
    private PlayerController playerCtr;
    private SoundManager soundMgr;
    [SerializeField]
    private Light childLight;
    void Start()
    {
        lookMonsterWait = new WaitForSeconds(lookMonster_time);
        cameraCtr = GameManager.Instance.cameraMgr.CameraCtr;
        cameraMgr = GameManager.Instance.cameraMgr;
        playableDir = GetComponent<PlayableDirector>();
        deadCollider = GetComponent<Collider>();
        playerCtr = GameManager.Instance.unitMgr.PlayerCtr;
        soundMgr = GameManager.Instance.soundMgr;
    }



    IEnumerator DieCam12F()
    {
        
        cameraMgr.PostProcess.AllBlackCloseCamera();
        yield return new WaitForSeconds(4f);
        deadCollider.enabled = true;
        childLight.enabled = true;
        cameraMgr.PostProcess.StartCoroutine(cameraMgr.PostProcess.VignetteOn(0f,4f,Color.black));
        yield return new WaitForSeconds(4.3f);
        cameraMgr.PostProcess.StartCoroutine(cameraMgr.PostProcess.VignetteOn());        
        playerCtr.PSound.StartCoroutine(playerCtr.PSound.On12HurtSound());
        cameraMgr.PostProcess.CurCoroutine.Add(StartCoroutine(cameraMgr.PostProcess.PostProccessEffectOn(EpostProcessType.Grain)));
        playableDir.Play();

        yield return null;

    }

    public IEnumerator DeadCam()
    {
        playableDir.Stop();
        Transform _target = GameManager.Instance.unitMgr.ContectTarget;
        Camera curCam = GameManager.Instance.cameraMgr.CurCamera;
        if (_target != null)
        {
            
            yield return StartCoroutine(CamMoveToTarget(curCam, _target, 10f, 10f));
            yield return new WaitForSeconds(0.5f);
            cameraCtr.StartCoroutine(cameraCtr.CameraShakeOrigin(cameraMgr.CurCamera, 0.5f, 0.8f));
            
        }
   
    }

    public IEnumerator CamMoveToTarget(Camera _cam, Transform _target, float _speed, float _rotSpeed , float _threshold = 0.5f)
    {
        _cam.transform.parent = _target;
        float disValue = 0f;
        float angleValue = 0f;
        
        while (true)
        {            

            _cam.transform.position = Vector3.Lerp(_cam.transform.position, _target.position - _target.forward  , Time.deltaTime * _speed);
            _cam.transform.rotation = Quaternion.Lerp(_cam.transform.rotation, _target.rotation, Time.deltaTime * _rotSpeed);
            disValue = Vector3.Distance(_cam.transform.position, _target.position - _target.forward);
            angleValue = Quaternion.Angle(_cam.transform.rotation, _target.rotation);
            if (disValue < _threshold && angleValue < _threshold)
            {
                break;
            }

            yield return null;

        }

       

    }




    public void ChangeTimeLineAsset()
    {
        playableDir.Stop();

    }



    public void Die12FDeadCam()
    {
        StartCoroutine(DieCam12F());
    }




    private void OnTriggerEnter(Collider other)
    {

        IEnemyContect col = other.GetComponent<IEnemyContect>();
        if(col != null)
        {
            col.OnContect();
            transform.GetComponent<Collider>().enabled = false;
        }
        

    }



}
