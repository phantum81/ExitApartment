using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    private SoundController soundCtr;
    public SoundController SoundCtr => soundCtr;
    private SoundManager soundMgr;
    void Start()
    {
        soundCtr= GetComponent<SoundController>();
        soundMgr = GameManager.Instance.soundMgr;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnGravitySound()
    {
        soundCtr.ChangeSound(soundMgr.SoundList[102]);
        soundCtr.SetVolume(0.7f);
        
    }



    public void On12FDeadSound()
    {
        soundCtr.ChangeSound(soundMgr.SoundList[101]);
        soundCtr.SetVolume(1f);
        
    }
    public IEnumerator On12HurtSound()
    {
        
        HurtBreathe();
        yield return new WaitUntil(() => !soundCtr.CheckPlaying());
        ScareBreath();
    }
    public void HurtBreathe()
    {
        soundCtr.ChangeSound(soundMgr.SoundList[104]);
        soundCtr.SetVolume(0.5f);
    }
    public void ScareBreath()
    {
        soundCtr.ChangeSound(soundMgr.SoundList[103], true);
        soundCtr.SetVolume(0.5f);
        
    }
}
