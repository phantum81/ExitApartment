using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MimicSpace
{
    /// <summary>
    /// This is a very basic movement script, if you want to replace it
    /// Just don't forget to update the Mimic's velocity vector with a Vector3(x, 0, z)
    /// </summary>
    public class Movement : MonoBehaviour
    {
        [Header("Controls")]
        [Tooltip("Body Height from ground")]
        [Range(0.5f, 5f)]
        public float height = 0.8f;
        
        Vector3 velocity = Vector3.zero;
        [Header("속력"),SerializeField]
        private float speed = 1f;
        public float velocityLerpCoef = 4f;
        Mimic myMimic;

        [Header("타겟"),SerializeField]
        private Transform target;

        [Header("타겟과의 제한거리"), SerializeField]
        private float validDis = 0.4f;

        [Header("회전속도"),SerializeField]
        private float rotSpeed = 3f;


        [Header("이동루트1"),SerializeField]
        private Transform route1;
        [Header("이동루트2"),SerializeField]
        private Transform route2;


        private void Start()
        {
            myMimic = GetComponent<Mimic>();
            
        }

        void Update()
        {



        }

        IEnumerator GravityDead()
        {
            
            
            Camera onDeadCam= GameManager.Instance.cameraMgr.CameraDic[1];
            yield return new WaitForSeconds(15f);
            while (Vector3.Distance(transform.position, onDeadCam.transform.position) > validDis)
            {
                ChaseTarget(transform, onDeadCam.transform, velocityLerpCoef);
                yield return null;
            }


        }
        
        IEnumerator Clear12F()
        {
            float dis1 = Vector3.Distance(transform.position, route1.position);
            float dis2 = Vector3.Distance(transform.position, route2.position);
            while (dis1 > 0.2f)
            {
                ChaseTarget(transform, route1.transform, speed);
                yield return null;
            }
            while (dis2 > 0.2f)
            {
                ChaseTarget(transform, route2.transform, speed);
                yield return null;
            }
        }


        public void ChaseTarget(Transform _chaser,Transform _target, float _speed )
        {
            Vector3 _dir = (_target.position - _chaser.position).normalized;
            _chaser.rotation = Quaternion.Lerp(_chaser.rotation,Quaternion.LookRotation(_dir),Time.deltaTime* rotSpeed);
            velocity = _chaser.forward;
            


            _chaser.position = _chaser.position + velocity * Time.deltaTime* _speed;

            RaycastHit hit;

            Vector3 destHeight = _chaser.position;

            if (Physics.Raycast(_chaser.position + Vector3.up, -Vector3.up, out hit))
                destHeight = new Vector3(_chaser.position.x, hit.point.y + height, _chaser.position.z);

            _chaser.position = Vector3.Lerp(_chaser.position, destHeight, velocityLerpCoef * Time.deltaTime);
            
        }



        public void OnDead12F()
        {
            StartCoroutine(GravityDead());
        }

        public void OnClear12F()
        {
            StartCoroutine(Clear12F());
        }
    }

}