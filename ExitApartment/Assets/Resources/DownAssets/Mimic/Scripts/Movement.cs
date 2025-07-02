using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        

        [Header("타겟"),SerializeField]
        private Transform target;

        [Header("타겟과의 제한거리"), SerializeField]
        private float validDis = 0.4f;

        [Header("플레이어 접촉 제한거리"), SerializeField]
        private float playerValidDis = 0.4f;

        [Header("회전속도"),SerializeField]
        private float rotSpeed = 3f;


        [Header("이동루트1"),SerializeField]
        private Transform route1;
        [Header("이동루트2"), SerializeField]
        private Transform route2;

        private UnitManager unitMgr;
        private CameraManager cameraMgr;
        private SoundController soundCtr;
        private SoundManager soundMgr;
        private void Start()
        {
            unitMgr = GameManager.Instance.unitMgr;
            cameraMgr= GameManager.Instance.cameraMgr;
            soundCtr = gameObject.GetComponent<SoundController>();
            soundMgr = GameManager.Instance.soundMgr;
            soundCtr.AudioPath = GameManager.Instance.soundMgr.SoundList[81];
        }





        IEnumerator GravityDead()
        {
         
            Camera onDeadCam= GameManager.Instance.cameraMgr.CameraDic[1];
            yield return new WaitForSeconds(20f);
            soundCtr.Play();
            StartCoroutine(cameraMgr.CameraCtr.CameraShake(cameraMgr.CurCamera, 1.3f, 0.3f));
            soundMgr.BgmCtr.BgmChange(soundMgr.SoundList[82], false);
            while (Vector3.Distance(transform.position, route1.transform.position) > validDis)
            {                
                unitMgr.MobCtr.ChaseTarget(transform, route1.transform, speed, rotSpeed);
                yield return null;
            }

        }

             



        IEnumerator Clear12F()
        {
            GameManager.Instance.Set12FClearFloor(true);
            GameManager.Instance.ChangeFloorLevel(EFloorType.Mob122F);
            WaitUntil openWait =  new WaitUntil(() => GameManager.Instance.unitMgr.ElevatorCtr.eleWork == EElevatorWork.Opening);
            yield return new WaitForSeconds(4f);
            soundCtr.Play();
            while (Vector3.Distance(transform.position, route1.position)> validDis)
            {
                unitMgr.MobCtr.ChaseTarget(transform, route1.transform, speed*2f, rotSpeed);
                yield return null;
            }

            soundCtr.Play();
            StartCoroutine(cameraMgr.CameraCtr.CameraShake(cameraMgr.CurCamera, 4f, 0.05f));
            soundMgr.BgmCtr.BgmChange(soundMgr.SoundList[82], false);
            while (true)
            {
                if(GameManager.Instance.unitMgr.ElevatorCtr.eleWork == EElevatorWork.Opening)
                {                    
                    unitMgr.MobCtr.ChaseTarget(transform, target, speed * 2f, rotSpeed);
                    if(Vector3.Distance(transform.position, target.position) < playerValidDis)
                        break;
                }
                else if (GameManager.Instance.unitMgr.ElevatorCtr.eleWork == EElevatorWork.Closing)
                {

                    unitMgr.MobCtr.ChaseTarget(transform, route2, speed * 2f, rotSpeed);
                    if (Vector3.Distance(transform.position, route2.position) < validDis)
                    {
                        yield return new WaitUntil(()=>GameManager.Instance.unitMgr.ElevatorCtr.IsClose);
                        GameManager.Instance.Set12FClearFloor(true);
                        transform.gameObject.SetActive(false);
                        GameManager.Instance.ChangeFloorLevel(EFloorType.Mob122F);
                        break;
                    }
                        
                }
                    

                yield return null;
            }
            
        }
        

        #region 이벤트 등록부

        public void OnDead12F()
        {
            StartCoroutine(GravityDead());
        }

        public void OnClear12F()
        {
            StartCoroutine(Clear12F());

        }
    }
    #endregion
}