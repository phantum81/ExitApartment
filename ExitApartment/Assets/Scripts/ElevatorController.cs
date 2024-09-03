using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorController : MonoBehaviour
{
    /// <summary>
    /// 0 = 왼쪽 1= 오른쪽
    /// </summary>
    [Header("엘리베이터 문"), SerializeField]
    private GameObject[] doors;
    [Header("문 속도"), SerializeField]
    private float speed = 3f;

    private Vector3[] origin = new Vector3[2];

    public EElevatorWork eleWork = EElevatorWork.Closing;
    public EFloorType eCurFloor = EFloorType.Home15EB;
    private Coroutine curCoroutine;
    public Coroutine CurCoroutine { get; set; }


    private SoundController soundCtr;
    private UnitManager unitMgr;
    private CameraManager cameraMgr;
    private SoundManager soundMgr;
    private bool isClose = true;
    public bool IsClose => isClose;
    private Vector3 originPos = Vector3.zero;
    void Start()
    {
        Init();
        //StartCoroutine(ShakeElevator(2f,0.005f));
    }

    private void Update()
    {

        
    }

    public IEnumerator OpenDoor()
    {

        float elapsedTime = 0f;
        float duration = 5f;
        float smoothTime = 0.7f; // 문이 닫히는 속도를 부드럽게 조정하기 위한 값
        isClose = false;
        yield return new WaitForSeconds(0.5f);
        soundCtr.Play();

        // 문들의 시작 위치를 저장합니다.
        Vector3 door0StartPos = doors[0].transform.position;
        Vector3 door1StartPos = doors[1].transform.position;

        // 목표 위치를 설정합니다.
        Vector3 door0TargetPos = origin[0] + Vector3.left * 0.8f;
        Vector3 door1TargetPos = origin[1] + Vector3.right * 0.8f;

        Vector3 door0Velocity = Vector3.zero;
        Vector3 door1Velocity = Vector3.zero;

        while (elapsedTime < duration)
        {
            // 문들이 목표 위치로 이동하도록 합니다.
            doors[0].transform.position = Vector3.SmoothDamp(doors[0].transform.position, door0TargetPos, ref door0Velocity, smoothTime);
            doors[1].transform.position = Vector3.SmoothDamp(doors[1].transform.position, door1TargetPos, ref door1Velocity, smoothTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 문들이 정확히 목표 위치에 도달하도록 설정합니다.
        doors[0].transform.position = door0TargetPos;
        doors[1].transform.position = door1TargetPos;

        isClose = true;
        curCoroutine = null;

    }

    public IEnumerator CloseDoor()
    {



        float elapsedTime = 0f;
        float duration = 5f;
        float smoothTime = 0.7f; // 문이 닫히는 속도를 더 부드럽게 조정하기 위한 값
        yield return new WaitForSeconds(0.5f);
        soundCtr.Play();

        Vector3 door0StartPos = doors[0].transform.position;
        Vector3 door1StartPos = doors[1].transform.position;

        Vector3 door0Velocity = Vector3.zero;
        Vector3 door1Velocity = Vector3.zero;

        while (elapsedTime < duration)
        {
            doors[0].transform.position = Vector3.SmoothDamp(doors[0].transform.position, origin[0], ref door0Velocity, smoothTime);
            doors[1].transform.position = Vector3.SmoothDamp(doors[1].transform.position, origin[1], ref door1Velocity, smoothTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the doors are exactly at the target position at the end
        doors[0].transform.position = origin[0];
        doors[1].transform.position = origin[1];

        isClose = true;
        curCoroutine = null;


    }
    private void Init()
    {
        DoorOriginInit();
        soundCtr = gameObject.GetComponent<SoundController>();
        soundCtr.AudioPath = GameManager.Instance.soundMgr.SoundList[35];
        unitMgr = GameManager.Instance.unitMgr;
        cameraMgr = GameManager.Instance.cameraMgr;
        originPos = transform.position;
        soundMgr = GameManager.Instance.soundMgr;

    }
    private void DoorOriginInit()
    {
        for (int i = 0; i < doors.Length; i++)
        {
            origin[i] = doors[i].transform.position;
        }
    }
    

    public IEnumerator ShakeElevator(float _shakeTime, float _shakeAmount)
    {
        float curTime = 0f;
        Vector3 originPos = transform.position;
        while (curTime<_shakeTime)
        {
            transform.position = originPos + Random.insideUnitSphere * _shakeAmount;
            curTime += Time.deltaTime;
            yield return null;
        }

        transform.position = originPos;
    }

    public IEnumerator MoveFloor()
    {
        yield return new WaitUntil(() => isClose == true);
        
        string floor = UiManager.Instance.inGameCtr.InGameUiShower.GetCurFloor();
        UpdateCurFloor(floor);
        DoorOriginInit();
        StartCoroutine(ShakeElevator(2f, 0.005f));
        yield return new WaitForSeconds(2f);
        eleWork = EElevatorWork.Closing;
    }


    private void UpdateCurFloor(string _floor)
    {
        unitMgr.PlayerCtr.Player.transform.parent = transform;
        cameraMgr.CurCamera.transform.parent = transform;
        
        switch (_floor)
        {

            case UnitManager.HOME_FLOOR:
                eCurFloor = EFloorType.Home15EB;                
                unitMgr.ChangeFloor(eCurFloor);
                transform.position = originPos;                
                cameraMgr.PostProcess.SetMotionBlur(true);
                cameraMgr.PostProcess.OriginSetting();
                soundMgr.BgmCtr.StopBgm();
                unitMgr.SkyChange(unitMgr.SkyBox);
                break;
            case UnitManager.LOCKED_FLOOR:
                eCurFloor = EFloorType.Nothing436A;
                unitMgr.ChangeFloor(eCurFloor);
                transform.position = originPos;
                GameManager.Instance.ChangeFloorLevel(eCurFloor);
                cameraMgr.PostProcess.SetMotionBlur(true);
                cameraMgr.PostProcess.OriginSetting();
                soundMgr.BgmCtr.StopBgm();
                unitMgr.SkyChange(unitMgr.SkyBox);
                break;
            case UnitManager.Fall_FLOOR:
                eCurFloor = EFloorType.Mob122F;
                unitMgr.ChangeFloor(eCurFloor);
                transform.position = unitMgr.ElevatorSpawnDic[eCurFloor].position;
                cameraMgr.PostProcess.SetMotionBlur(true);
                cameraMgr.PostProcess.OriginSetting();
                soundMgr.BgmCtr.StopBgm();
                unitMgr.SkyChange(unitMgr.SkyBox);
                break;
            case UnitManager.FOREST_FLOOR:
                eCurFloor = EFloorType.Forest5ABC;
                unitMgr.ChangeFloor(eCurFloor);
                transform.position = originPos;
                cameraMgr.PostProcess.SetMotionBlur(false);
                cameraMgr.PostProcess.OriginSetting();
                soundMgr.BgmCtr.StopBgm();
                unitMgr.SkyChange(unitMgr.SkyBox);
                break;
            case UnitManager.ESCAPE_FLOOR:
                eCurFloor = EFloorType.Escape888B;                
                unitMgr.ChangeFloor(eCurFloor);
                transform.position = unitMgr.ElevatorSpawnDic[eCurFloor].position;
                cameraMgr.PostProcess.SetMotionBlur(true);
                cameraMgr.PostProcess.EscapeSetting();
                soundMgr.BgmCtr.BgmChange(soundMgr.SoundList[250], true, 0.1f);
                unitMgr.SkyChange(unitMgr.EscapeSkyBox);
                break;
            case UnitManager.LOBBY_FLOOR:
                eCurFloor = EFloorType.Looby;
                unitMgr.ChangeFloor(eCurFloor);
                transform.position = unitMgr.ElevatorSpawnDic[eCurFloor].position;
                GameManager.Instance.ChangeFloorLevel(eCurFloor);
                cameraMgr.PostProcess.SetMotionBlur(true);
                cameraMgr.PostProcess.EscapeSetting();
                cameraMgr.PostProcess.LobbySetting();
                unitMgr.SkyChange(unitMgr.EscapeSkyBox);
                soundMgr.BgmCtr.StopBgm();
                break;
            default:

                break;
        }
        unitMgr.PlayerCtr.Player.transform.parent = unitMgr.PlayerCtr.transform;
        cameraMgr.CurCamera.transform.parent = null;
    }
    public void OnEndGame()
    {
        StartCoroutine(CloseDoor());       

    }



    
}
