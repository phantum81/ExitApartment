using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    [Header("플레이어 컨트롤러"), SerializeField]
    private PlayerController playerCtr;
    public PlayerController PlayerCtr => playerCtr;
    [Header("엘리베이터 컨트롤러"), SerializeField]
    private ElevatorController elevatorCtr;
    public ElevatorController ElevatorCtr => elevatorCtr;

    [Header("충돌한 에너미 타겟"), SerializeField]
    private Transform contectTarget;
    public Transform ContectTarget => contectTarget;

    public List<GameObject> floorList;

    private Vector3 reserveGravity = new Vector3(0, 0, 1f);
    public Vector3 ReserveGravity => reserveGravity;

    void Start()
    {
        reserveGravity.Normalize();
        playerCtr.Init();
    }

    // Update is called once per frame
    void Update()
    {

    }


    private IEnumerator ChangeGravity(Rigidbody _rigd, Vector3 _gravity, float _startSpeed)
    {
        EventManager evMgr = GameManager.Instance.eventMgr;


        while (evMgr.eCurEvent == ESOEventType.OnGravity || evMgr.eCurEvent == ESOEventType.OnClear12F)
        {
            _rigd.useGravity = false;
            _startSpeed = Mathf.Clamp(_startSpeed, 0, 9.81f);
            _startSpeed += Time.deltaTime;
            _rigd.AddForce(_gravity * _startSpeed * _rigd.mass * 0.2f, ForceMode.Acceleration);
            yield return null;
        }
    }

    public void OnChangeGravity(Rigidbody _rigd, Vector3 _gravity, float _startSpeed)
    {
        StartCoroutine(ChangeGravity(_rigd, _gravity, _startSpeed));
    }




    public void GetContectTarget(Transform _target)
    {
        contectTarget = _target;
    }


    public void ShowMob(Transform _enemy, bool _bool)
    {
        _enemy.gameObject.SetActive(_bool);
    }



}
