using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    [Header("�÷��̾� ��Ʈ�ѷ�"), SerializeField]
    private PlayerController playerCtr;
    public PlayerController PlayerCtr=>playerCtr;
    [Header("���������� ��Ʈ�ѷ�"), SerializeField]
    private ElevatorController elevatorCtr;
    public ElevatorController ElevatorCtr => elevatorCtr;

    [Header("�浹�� ���ʹ� Ÿ��"), SerializeField]
    private Transform contectTarget;
    public Transform ContectTarget => contectTarget;

    void Start()
    {
        playerCtr.Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }




    public IEnumerator ChangeGravity (Rigidbody _rigd, Vector3 _gravity)
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


    public void GetContectTarget(Transform _target)
    {
        contectTarget = _target;
    }


}
