using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMentalDamage<T> : IState<T> where T : MonoBehaviour
{
    private float m_damageTime;
    public void OperateEnter(T _send)
    {
        if (_send is PlayerPostProcess)
        {
            PlayerPostProcess _post = _send as PlayerPostProcess;
            _post.MentalDamagePostProccess();
        }

    }

    public void OperateUpdate(T _send)
    {
        m_damageTime += Time.deltaTime;
        if (m_damageTime > 4f)
        {
            GameManager.Instance.eventMgr.ChangePlayerState(EplayerState.None);
            m_damageTime= 0f;
        }
    }

    public void OperateExit(T _send)
    {

    }
}
