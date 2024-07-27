using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorController : MonoBehaviour
{
    /// <summary>
    /// 0 = ���� 1= ������
    /// </summary>
    [Header("���������� ��"), SerializeField]
    private GameObject[] doors;
    [Header("�� �ӵ�"), SerializeField]
    private float speed = 3f;

    private Vector3[] origin = new Vector3[2];

    public EElevatorWork eleWork = EElevatorWork.Closing;
    public EFloorType eCurFloor = EFloorType.Home15EB;
    private Coroutine curCoroutine;
    public Coroutine CurCoroutine { get; set; }
    
    private SoundController soundCtr;
    private UnitManager unitMgr;
    private bool isClose = true;
    public bool IsClose => isClose;
    
    void Start()
    {
        Init();
        //StartCoroutine(ShakeElevator(2f,0.005f));
    }

    // Update is called once per frame
    void Update()
    {
       
    }


    public IEnumerator OpenDoor()
    {

        //float elapsedTime = 0f;
        //float duration = 5f;
        //isClose = false;
        //yield return new WaitForSeconds(0.5f);
        //soundCtr.Play();
        //while (elapsedTime < duration)
        //{
        //    doors[0].transform.position = Vector3.Lerp(doors[0].transform.position, origin[0] + Vector3.left * 0.8f, (elapsedTime / duration) * Time.fixedDeltaTime * speed);
        //    doors[1].transform.position = Vector3.Lerp(doors[1].transform.position, origin[1] + Vector3.right * 0.8f, (elapsedTime / duration) * Time.fixedDeltaTime * speed);
        //    elapsedTime += Time.deltaTime;
        //    yield return null;
        //}
        //curCoroutine= null;

        float elapsedTime = 0f;
        float duration = 5f;
        float smoothTime = 0.7f; // ���� ������ �ӵ��� �ε巴�� �����ϱ� ���� ��
        isClose = false;
        yield return new WaitForSeconds(0.5f);
        soundCtr.Play();

        // ������ ���� ��ġ�� �����մϴ�.
        Vector3 door0StartPos = doors[0].transform.position;
        Vector3 door1StartPos = doors[1].transform.position;

        // ��ǥ ��ġ�� �����մϴ�.
        Vector3 door0TargetPos = origin[0] + Vector3.left * 0.8f;
        Vector3 door1TargetPos = origin[1] + Vector3.right * 0.8f;

        Vector3 door0Velocity = Vector3.zero;
        Vector3 door1Velocity = Vector3.zero;

        while (elapsedTime < duration)
        {
            // ������ ��ǥ ��ġ�� �̵��ϵ��� �մϴ�.
            doors[0].transform.position = Vector3.SmoothDamp(doors[0].transform.position, door0TargetPos, ref door0Velocity, smoothTime);
            doors[1].transform.position = Vector3.SmoothDamp(doors[1].transform.position, door1TargetPos, ref door1Velocity, smoothTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // ������ ��Ȯ�� ��ǥ ��ġ�� �����ϵ��� �����մϴ�.
        doors[0].transform.position = door0TargetPos;
        doors[1].transform.position = door1TargetPos;

        isClose = true;
        curCoroutine = null;

    }

    public IEnumerator CloseDoor()
    {

        //float elapsedTime = 0f;
        //float duration = 5f;
        //yield return new WaitForSeconds(0.5f);
        //soundCtr.Play();
        //while (elapsedTime < duration)
        //{

        //    doors[0].transform.position = Vector3.Lerp(doors[0].transform.position, origin[0], (elapsedTime / duration) * Time.fixedDeltaTime * speed);
        //    doors[1].transform.position = Vector3.Lerp(doors[1].transform.position, origin[1], (elapsedTime / duration) * Time.fixedDeltaTime * speed);
        //    elapsedTime += Time.deltaTime;
        //    yield return null;
        //}
        //isClose = true;
        //curCoroutine = null;

        float elapsedTime = 0f;
        float duration = 5f;
        float smoothTime = 0.7f; // ���� ������ �ӵ��� �� �ε巴�� �����ϱ� ���� ��
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
        for(int i =0; i < doors.Length;i++)
        {
            origin[i] = doors[i].transform.position;
        }
        soundCtr = gameObject.GetComponent<SoundController>();
        soundCtr.AudioPath = GameManager.Instance.soundMgr.SoundList[35];
        unitMgr = GameManager.Instance.unitMgr;
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
        StartCoroutine(ShakeElevator(2f, 0.005f));
        string floor = UiManager.Instance.inGameCtr.InGameUiShower.GetCurFloor();
        UpdateCurFloor(floor);
        yield return new WaitForSeconds(2f);
        eleWork = EElevatorWork.Closing;
    }


    private void UpdateCurFloor(string _floor)
    {
        switch (_floor)
        {
            case UnitManager.HOME_FLOOR:
                eCurFloor = EFloorType.Home15EB;
                unitMgr.ChangeFloor(eCurFloor);
                GameManager.Instance.cameraMgr.PostProcess.SetMotionBlur(true);
                break;
            case UnitManager.LOCKED_FLOOR:
                eCurFloor = EFloorType.Nothing436A;
                unitMgr.ChangeFloor(eCurFloor);
                GameManager.Instance.ChangeFloorLevel(eCurFloor);
                GameManager.Instance.cameraMgr.PostProcess.SetMotionBlur(true);
                break;
            case UnitManager.Fall_FLOOR:
                eCurFloor = EFloorType.Mob122F;
                unitMgr.ChangeFloor(eCurFloor);
                GameManager.Instance.cameraMgr.PostProcess.SetMotionBlur(true);
                break;
            case UnitManager.FOREST_FLOOR:
                eCurFloor = EFloorType.Forest5ABC;
                unitMgr.ChangeFloor(eCurFloor);
                GameManager.Instance.cameraMgr.PostProcess.SetMotionBlur(false);
                break;
            case UnitManager.ESCAPE_FLOOR:
                eCurFloor = EFloorType.Escape888B;
                unitMgr.ChangeFloor(eCurFloor);
                GameManager.Instance.cameraMgr.PostProcess.SetMotionBlur(true);
                break;
            default:

                break;
        }
    }
    public void OnCloseDoor()
    {
        StartCoroutine(CloseDoor());
    }



}
