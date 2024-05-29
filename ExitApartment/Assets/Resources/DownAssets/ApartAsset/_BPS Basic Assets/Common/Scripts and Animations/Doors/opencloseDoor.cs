using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SojaExiles

{
	public class opencloseDoor : MonoBehaviour, IInteraction
	{
		
		private Animator openandClose;
		private bool isOpen;
		private InputManager inputMgr;
		void Start()
		{
            openandClose = GetComponent<Animator>();
			inputMgr = GameManager.Instance.inputMgr;
			isOpen = false;
		}


        public void Init()
        {

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
                }
            }
            else
            {
                if (isOpen)
                {
                    if (inputMgr.InputDic[EuserAction.Interaction])
                    {
                        StartCoroutine(closing());
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