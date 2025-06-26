using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerStateMentalDamage<T> : IState<T> where T : MonoBehaviour
{
    private float m_damageTime;
    public void OperateEnter(T _send)
    {
        if (_send is PlayerPostProcess _post)
        {
            _post.MentalDamagePostProccess();
            PlayerSound sound = _post.gameObject.GetComponent<PlayerController>().PSound;
            sound.OnMentalSound();
        }

    }

    public void OperateUpdate(T _send)
    {
        if (_send is PlayerPostProcess _post)
        {
            m_damageTime += Time.deltaTime;
            
            
            if (m_damageTime > 4f)
            {
               
                GameManager.Instance.eventMgr.ChangePlayerState(EplayerState.None);
                m_damageTime = 0f;
            }
        }

    }

    public void OperateExit(T _send)
    {
        if (_send is PlayerPostProcess _post)
        {
            PlayerSound sound = _post.gameObject.GetComponent<PlayerController>().PSound;
            sound.SoundCtr.Stop();
        }

    }

}
