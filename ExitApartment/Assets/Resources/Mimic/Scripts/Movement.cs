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
        public float speed = 5f;
        Vector3 velocity = Vector3.zero;
        public float velocityLerpCoef = 4f;
        Mimic myMimic;

        [Header("鸥百"),SerializeField]
        private Transform target;
        [Header("鸥百苞狼 力茄芭府"), SerializeField]
        private float validDis = 0.4f;

        private void Start()
        {
            myMimic = GetComponent<Mimic>();
            StartCoroutine( GravityDead());
        }

        void Update()
        {



        }

        IEnumerator GravityDead()
        {
            
            Vector3 dir = Vector3.zero;
            Camera onDeadCam= GameManager.Instance.cameraMgr.CameraDic[1];
            while (Vector3.Distance(transform.position, onDeadCam.transform.position) > validDis)
            {
                ChaseTarget(transform, onDeadCam.transform, dir);
                yield return null;
            }


        }
        
        IEnumerator KillTarget()
        {
            while (true)
            {

                yield return null;
            }
        }


        public void ChaseTarget(Transform _chaser,Transform _target, Vector3 _dir )
        {
            _dir = (_target.position - _chaser.position).normalized;
            _chaser.rotation = Quaternion.LookRotation(_dir);
            velocity = _chaser.forward;
            //myMimic.velocity = velocity;


            _chaser.position = _chaser.position + velocity * Time.deltaTime;

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
    }

}