using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TrapDoor : ZoomableItem
{
    [Header("Æ®·¦ ±ôºý ÀÌº¥Æ®")]
    public UnityEvent<bool> onTrapDoor;
    [Header("¸÷ º¸¿©ÁÖ´Â ÀÌº¥Æ®")]
    public UnityEvent<Transform, bool> onShowPumpkin;
    [Header("º¸¿©ÁÙ Å¸°Ù"),SerializeField]
    private Transform showTarget;
    private float trapCount = 0;
    public float TrapCount => trapCount;
    
    public override void Init()
    {
        base.Init();
    }

    public override void OnInteraction(Vector3 _angle)
    {
        base.OnInteraction(_angle);
        
        if(trapCount == 1 )
        {
            onTrapDoor.Invoke(true);
        }
        else if(trapCount == 2)
        {
            onShowPumpkin.Invoke(showTarget, true);
            GameManager.Instance.eventMgr.ChangePlayerState(EplayerState.MentalDamage);
        }
        else if(trapCount == 3)
        {
            onTrapDoor.Invoke(false);
            onShowPumpkin.Invoke(showTarget, false);
            
        }

        trapCount++;
    }
    
}
