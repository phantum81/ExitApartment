using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob12F : MonoBehaviour, IEnemyContect
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnContect()
    {
        GameManager.Instance.unitMgr.GetContectTarget(this.transform);
        GameManager.Instance.eventMgr.ChangePlayerState(EplayerState.Die);
    }


}
