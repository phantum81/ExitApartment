using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TrapDoor : ZoomableItem
{
    [Header("Ʈ�� ���� �̺�Ʈ")]
    public UnityEvent<bool> onTrapDoor;
    [Header("�� �����ִ� �̺�Ʈ")]
    public UnityEvent<Transform, bool> onShowPumpkin;
    [Header("������ Ÿ��"),SerializeField]
    private Transform showTarget;

    private SoundController soundCtr;
    private float trapCount = 0;
    public float TrapCount => trapCount;
    
    public override void Init()
    {
        base.Init();
        soundCtr = gameObject.GetComponent<SoundController>();
        soundCtr.AudioPath = GameManager.Instance.soundMgr.SoundList[36];
    }

    public override void OnInteraction(Vector3 _angle)
    {
        base.OnInteraction(_angle);
        if (GameManager.Instance.eventMgr.GetIsPumpkinEvent()) return;
        if(trapCount == 1 )
        {
            onTrapDoor.Invoke(true);
        }
        else if(trapCount == 2)
        {
            onShowPumpkin.Invoke(showTarget, true);
            soundCtr.Play();
            
        }
        else if(trapCount == 3)
        {
            onTrapDoor.Invoke(false);
            onShowPumpkin.Invoke(showTarget, false);
            GameManager.Instance.eventMgr.SetIsPumpkinEvent(true);
            GameManager.Instance.Save(GameManager.Instance.eFloorType, true);
        }

        trapCount++;
    }
    public override EInteractionType OnGetType()
    {
        return base.OnGetType();
    }

}
