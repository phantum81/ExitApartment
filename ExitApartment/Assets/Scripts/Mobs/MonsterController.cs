using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class MonsterController : MonoBehaviour
{

    EventManager eventMgr;
    UnitManager unitMgr;


    [Header("홈 이벤트 타겟 위치"), SerializeField]
    private Transform home_target;

    private float randomEventTime;
    private float appearTime = 3f;
    private float disappearTime = 5f;
    [Header("블링크 이벤트")]
    public List<UnityEvent<bool>> blinkEvent = new List<UnityEvent<bool>>();

   
    void Start()
    {
        eventMgr = GameManager.Instance.eventMgr;
        unitMgr= GameManager.Instance.unitMgr;
        StartCoroutine(PumpkinEvent());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator PumpkinEvent()
    {
        Transform pumpkinTransform = unitMgr.MobDic[EMobType.Pumpkin].transform;
        Transform pumpkinNote = unitMgr.NotePaperDic[ENoteType.Pumpkin].transform;
        while (true)
        {
            yield return new WaitUntil(() => eventMgr.GetIsPumpkinEvent());

            unitMgr.ShowObject(pumpkinNote, true);

            randomEventTime = Random.Range(20f, 50f);
            
            yield return new WaitForSeconds(randomEventTime);

            SetBlinkEvent(true);

            yield return new WaitForSeconds(appearTime);

            unitMgr.ShowObject(pumpkinTransform, true);
            pumpkinTransform.transform.position = home_target.position;
            pumpkinTransform.transform.rotation = home_target.rotation;


            yield return new WaitForSeconds(disappearTime);

            SetBlinkEvent(false);
            unitMgr.ShowObject(pumpkinTransform, false);
           

            yield return null;

        }
    }

    private void SetBlinkEvent(bool _bool)
    {
        foreach (var blink in blinkEvent)
        {
            blink.Invoke(_bool);
        }
        
    }

    public void ChaseTarget(Transform _chaser, Transform _target, float _speed, float _rotSpeed, float _height, float _velocityLerpCoef)
    {
        Vector3 velocity = Vector3.zero;
        Vector3 _dir = (_target.position - _chaser.position).normalized;
        _chaser.rotation = Quaternion.Lerp(_chaser.rotation, Quaternion.LookRotation(_dir), Time.deltaTime * _rotSpeed);
        velocity = _chaser.forward;

        _chaser.position = _chaser.position + velocity * Time.deltaTime * _speed;

        RaycastHit hit;
        Vector3 destHeight = _chaser.position;

        if (Physics.Raycast(_chaser.position + Vector3.up, -Vector3.up, out hit))
            destHeight = new Vector3(_chaser.position.x, hit.point.y + _height, _chaser.position.z);

        _chaser.position = Vector3.Lerp(_chaser.position, destHeight, _velocityLerpCoef * Time.deltaTime);

    }



    public Transform GetSoundtarget(Transform _origin, float _radius, int _layMaks)
    {
        Collider[] col = Physics.OverlapSphere(_origin.position, _radius, _layMaks);

        if (col != null)
        {
            foreach (Collider collider in col)
            {
                if (collider.GetComponent<AudioSource>() != null && collider.GetComponent<AudioSource>().isPlaying)
                    return collider.transform;
            }
            return null;
        }
        return null;

    }

    public Transform GetLookingTarget(Transform _origin, float _radius, int _layMaks)
    {
        Collider[] col = Physics.OverlapSphere(_origin.position, _radius, _layMaks);

        if (col != null)
        {
            foreach (Collider collider in col)
            {
                if ((1<<collider.gameObject.layer) == _layMaks)
                    return collider.transform;
            }
            return null;
        }
        return null;

    }

    public bool CheckTargetInSight(Transform _origin,Transform _target, float _viewAngle)
    {
        if(_target != null)
        {
            Vector3 dirToTarget = (_target.position - _origin.position).normalized;

            if(Vector3.Angle(_origin.forward, dirToTarget) < _viewAngle / 2)
            {
                //if (!Physics.Raycast(_origin.position, dirToTarget, Vector3.Distance(_target.position, _origin.position), 1 << 9))
                    return true;
            }
            return false;
        }
        return false;
    }
}
