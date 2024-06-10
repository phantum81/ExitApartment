using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SojaExiles

{

	public class Drawer_Pull_X : MonoBehaviour, IInteraction
    {

        private Animator openandClose;
        private bool isOpen;
        private InputManager inputMgr;
        private SoundController soundCtr;
        void Start()
        {

        }


        public void Init()
        {
            openandClose = GetComponent<Animator>();
            inputMgr = GameManager.Instance.inputMgr;
            isOpen = false;
            soundCtr = GetComponent<SoundController>();
        }

        public void OnRayHit(Color _color)
        {

        }
        public void OnInteraction(Vector3 _angle)
        {
            if (!isOpen)
            {
                if (inputMgr.InputDic[EuserAction.Interaction])
                {
                    StartCoroutine(opening());
                    soundCtr.AudioPath = GameManager.Instance.soundMgr.SoundList[32];
                    soundCtr.Play();
                }
            }
            else
            {
                if (isOpen)
                {
                    if (inputMgr.InputDic[EuserAction.Interaction])
                    {
                        StartCoroutine(closing());
                        soundCtr.AudioPath = GameManager.Instance.soundMgr.SoundList[32];
                        soundCtr.Play();
                    }
                }

            }
        }

        public EInteractionType OnGetType()
        {
            if(isOpen)
                return EInteractionType.Close;
            else 
                return EInteractionType.Open;
            
        }

        public void OnRayOut()
        {

        }

        IEnumerator opening()
		{
			print("you are opening the door");
            openandClose.Play("openpull_01");
            isOpen = true;
			yield return new WaitForSeconds(.5f);
		}

		IEnumerator closing()
		{
			print("you are closing the door");
            openandClose.Play("closepush_01");
            isOpen = false;
			yield return new WaitForSeconds(.5f);
		}


	}
}