using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgmController : MonoBehaviour
{
    private SoundController soundCtr;

    private string curPath;
    void Start()
    {
        soundCtr = GetComponent<SoundController>();
        GameManager.Instance.onForestFloor += StopBgm;
    }

    // Update is called once per frame
    void Update()
    {
        curPath = soundCtr.AudioPath;
    }

    public void OnChaseBgmOn()
    {
        
        BgmChange(GameManager.Instance.soundMgr.SoundList[80], true);

    }


    public void BgmChange(string _path, bool _loop, float _volume = 1f)
    {
        if(curPath != _path)
        {
            soundCtr.Stop();
            soundCtr.AudioPath = _path;
            soundCtr.SetVolume(_volume);
            soundCtr.Play();
            soundCtr.SetLoop(_loop);
        }

    }

    public void StopBgm()
    {
        soundCtr.Stop();
    }


}
