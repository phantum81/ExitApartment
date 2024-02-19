using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDeadCameraController : MonoBehaviour
{
    
    private WaitForSeconds lookMonsterWait;
    [Header("회전기다림"), SerializeField]
    private float lookMonster_time=4f;
    private float validAngle = 0.4f;




    [Header("회전속도"), SerializeField]
    private float rotateSpeed = 4f;
    [Header("상하 움직임 속도"), SerializeField]
    private float _d_speed = 12f;
    public float  D_speed => _d_speed;
    [Header("상하 움직임 정도"), SerializeField]
    private float _d_shake = 0.0008f;
    public float D_shake => _d_shake;

    private CameraController cameraCtr;
    private CameraManager cameraMgr;



    void Start()
    {
        lookMonsterWait = new WaitForSeconds(lookMonster_time);
        cameraCtr = GameManager.Instance.cameraMgr.CameraCtr;
        cameraMgr = GameManager.Instance.cameraMgr;

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator DieCam12F()
    {

        yield return new WaitUntil(() => cameraMgr.CameraDic[1].enabled == true);
        Quaternion _target = Quaternion.Euler(new Vector3(0, 90f, -90f));
        Quaternion _secondTarget = Quaternion.Euler(new Vector3(0, 220f, -90f));
        Quaternion _thirdTarget = Quaternion.Euler(new Vector3(0, 30f, -90f));
        yield return lookMonsterWait;
        StartCoroutine(CamLookAt(cameraMgr.CameraDic[1], _target, rotateSpeed, 2));
        yield return lookMonsterWait;
        StartCoroutine(CamLookAt(cameraMgr.CameraDic[1], _secondTarget, rotateSpeed * 2, 0));
        yield return lookMonsterWait;
        StartCoroutine(CamLookAt(cameraMgr.CameraDic[1], _thirdTarget, rotateSpeed * 3, 2));

    }

    IEnumerator CamLookAt(Camera _cam , Quaternion _target, float _rotSpeed, int _version)
    {

        while (Quaternion.Angle(_cam.transform.rotation, _target) > validAngle)
        {
            _cam.transform.rotation = Quaternion.Lerp(transform.rotation, _target, _rotSpeed * Time.deltaTime);
            cameraCtr.FollowCamera(_cam, transform, _d_speed, _d_shake, _version);
            yield return null;
        }


    }

    public void Die12FDeadCam()
    {
        StartCoroutine(DieCam12F());
    }

}
