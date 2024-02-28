using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    [Header("플레이어 컨트롤러"), SerializeField]
    private PlayerController playerCtr;
    public PlayerController PlayerCtr=>playerCtr;
    [Header("엘리베이터 컨트롤러"), SerializeField]
    private ElevatorController elevatorCtr;
    public ElevatorController ElevatorCtr => elevatorCtr;

    [Header("충돌한 에너미 타겟"), SerializeField]
    private Transform contectTarget;
    public Transform ContectTarget => contectTarget;



    private Vector3 reserveGravity = new Vector3(0, 0, 1f);
    public Vector3 ReserveGravity=> reserveGravity;

    void Start()
    {
        reserveGravity.Normalize();
        playerCtr.Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }




    private IEnumerator ChangeGravity (Rigidbody _rigd, Vector3 _gravity)
    {
        EventManager evMgr = GameManager.Instance.eventMgr;
        float speed = 0f;
        yield return new WaitUntil (()=> evMgr.eStageState == EstageEventState.Eventing);
        while(evMgr.eStageState == EstageEventState.Eventing)
        {
            _rigd.useGravity = false;
            speed = Mathf.Clamp(speed, 0, 9.81f);
            speed += Time.deltaTime;
            _rigd.AddForce(_gravity * speed * _rigd.mass, ForceMode.Acceleration);
            yield return null;
        }
    }

    public void OnChangeGravity(Rigidbody _rigd, Vector3 _gravity)
    {
        StartCoroutine(ChangeGravity(_rigd, _gravity));
    }




    public void GetContectTarget(Transform _target)
    {
        contectTarget = _target;
    }


}
