using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


    void Start()
    {
        lookMonsterWait = new WaitForSeconds(lookMonster_time);
        cameraCtr = GameManager.Instance.cameraMgr.CameraCtr;
        cameraMgr = GameManager.Instance.cameraMgr;
                
    }



    IEnumerator DieCam12F()
    {

        yield return new WaitUntil(() => cameraMgr.CameraDic[1].enabled == true);
        transform.GetComponent<Collider>().enabled = true;
        Quaternion _firstTarget = Quaternion.Euler(new Vector3(0, 90f, 0f));
        Quaternion _secondTarget = Quaternion.Euler(new Vector3(0, 90f, -90f));
        Quaternion _thirdTarget = Quaternion.Euler(new Vector3(0, 173f, -90f));
        Quaternion _forthTarget = Quaternion.Euler(new Vector3(0, 90f, -90f));

        
        yield return lookMonsterWait;
        yield return StartCoroutine(CamLookAt(cameraMgr.CameraDic[1], _firstTarget, rotateSpeed, min_rotateSpeed, max_rotateSpeed));
        StartCoroutine(FollowCam(cameraMgr.CameraDic[1], transform, _d_speed, _d_shake, 2));

        yield return lookMonsterWait;
        yield return StartCoroutine(cameraCtr.ShakeRotateCam(cameraMgr.CameraDic[1], 2.5f, 40f, shakeDir, 12f));
        yield return StartCoroutine(CamLookAt(cameraMgr.CameraDic[1], _secondTarget, rotateSpeed*2f, min_rotateSpeed, max_rotateSpeed));

        yield return lookMonsterWait;
        yield return StartCoroutine(CamLookAt(cameraMgr.CameraDic[1], _thirdTarget, rotateSpeed*3f, min_rotateSpeed, max_rotateSpeed));

        yield return lookMonsterWait;
        yield return StartCoroutine(CamLookAt(cameraMgr.CameraDic[1], _forthTarget, rotateSpeed*6f , min_rotateSpeed, max_rotateSpeed));
        

    }
    public IEnumerator DeadCam()
    {
        
        Transform _target = GameManager.Instance.unitMgr.ContectTarget;
        yield return new WaitUntil(()=> _target != null);

        Vector3 dir = _target.position - cameraMgr.CurCamera.transform.position;
        Quaternion rotateDir =  Quaternion.LookRotation(dir);

        cameraMgr.CurCamera.transform.position = _target.position + _target.forward * 0.4f;
        cameraMgr.CurCamera.transform.rotation = rotateDir;
        StartCoroutine(cameraCtr.CameraShake(cameraMgr.CurCamera, 0.5f, 0.8f));


        
    }

    IEnumerator CamLookAt(Camera _cam , Quaternion _target, float _rotSpeed, float _minSpeed, float _maxSpeed)
    {
        float speed=0f;
        while (Quaternion.Angle(_cam.transform.rotation, _target) > validAngle)
        {
            speed += Time.deltaTime * _rotSpeed;
            Mathf.Clamp(speed, _minSpeed, _maxSpeed);
            _cam.transform.rotation = Quaternion.RotateTowards(transform.rotation, _target, speed * Time.deltaTime);
            
            yield return null;
        }


    }


    IEnumerator FollowCam(Camera _cam, Transform _target, float _speed, float _shake, int _version)
    {
        while (true)
        {
            cameraCtr.FollowCamera( _cam, _target, _speed, _shake, _version);
            
            yield return null;
        }
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
