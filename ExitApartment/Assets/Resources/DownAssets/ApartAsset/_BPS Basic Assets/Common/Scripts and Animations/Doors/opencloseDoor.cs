using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SojaExiles

{
	public class opencloseDoor : MonoBehaviour, IInteraction
	{
        public EDoorType eDoorType;
		private Animator openandClose;
		private bool isOpen;
        
		private InputManager inputMgr;
        private SoundController soundCtr;
		void Start()
		{

		}

        public void UnLockedDoor()
        {
            eDoorType = EDoorType.HomeDoor;
        }
        public void Init()
        {
            openandClose = GetComponent<Animator>();
            inputMgr = GameManager.Instance.inputMgr;
            soundCtr = gameObject.GetComponent<SoundController>();
            isOpen = false;
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
                    
                    switch (eDoorType)
                    {
                        case EDoorType.Closet:
                            soundCtr.AudioPath = GameManager.Instance.soundMgr.SoundList[32];
                            soundCtr.Play();
                            StartCoroutine(opening());
                            break;
                        case EDoorType.HomeDoor:
                            soundCtr.AudioPath = GameManager.Instance.soundMgr.SoundList[30];
                            soundCtr.Play();
                            StartCoroutine(opening());
                            break;
                        case EDoorType.Locked:
                            soundCtr.AudioPath = GameManager.Instance.soundMgr.SoundList[32];
                            soundCtr.Play();
                            break;
                    }

                }
            }
            else
            {
                if (isOpen)
                {
                    if (inputMgr.InputDic[EuserAction.Interaction])
                    {
                        
                        switch (eDoorType)
                        {
                            case EDoorType.Closet:
                                soundCtr.AudioPath = GameManager.Instance.soundMgr.SoundList[33];
                                soundCtr.Play();
                                StartCoroutine(closing());
                                break;
                            case EDoorType.HomeDoor:
                                soundCtr.AudioPath = GameManager.Instance.soundMgr.SoundList[31];
                                soundCtr.Play();
                                StartCoroutine(closing());
                                break;
                            case EDoorType.Locked:
                                soundCtr.AudioPath = GameManager.Instance.soundMgr.SoundList[32];
                                soundCtr.Play();
                                break;
                        }
                    }
                }

            }
        }

        public void OnRayOut()
        {

        }

        public EInteractionType OnGetType()
        {
            if (isOpen)
                return EInteractionType.Close;
            else
                return EInteractionType.Open;
        }


        IEnumerator opening()
        {
            print("you are opening the door");
            openandClose.Play("Opening");
            isOpen = true;
            yield return new WaitForSeconds(.5f);
        }

        IEnumerator closing()
        {
            print("you are closing the door");
            openandClose.Play("Closing");
            isOpen = false;
            yield return new WaitForSeconds(.5f);
        }


    }





}