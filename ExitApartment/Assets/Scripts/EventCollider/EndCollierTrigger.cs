using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements.Experimental;

public class EndCollierTrigger : MonoBehaviour
{
    public UnityEvent onCloseDoor;
    public UnityEvent<float,float> onScreenChange;
    public UnityEvent onChangeScene;
    private Coroutine screenChangeCoroutine;
    private Coroutine changeSceneCoroutine;

    private bool isDoit = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!isDoit)
        {
            isDoit = true;
            onCloseDoor.Invoke();
            screenChangeCoroutine = StartCoroutine(GameManager.Instance.CoTimer(3f, OnEndGame));

        }
    }

    private void OnEndGame()
    {
        onScreenChange.Invoke(1f,1f);
        if(GameManager.Instance.AchieveData.DieCount == 0)
            GameManager.Instance.achievementCtr.Unlock(ConstBundle.NO_DIE_CLEAR);
        changeSceneCoroutine = StartCoroutine(GameManager.Instance.CoTimer(5f, OnChangeScene));
    }
    private void OnChangeScene()
    {
        onChangeScene.Invoke();
    }
}
