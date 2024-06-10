using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private Dictionary<string, AudioClip> audioDic = new Dictionary<string, AudioClip>();
    public Dictionary<string, AudioClip> AudioDic => audioDic;


    private Dictionary<int, string> soundList = new Dictionary<int, string>();
    /// <summary>
    /// 0~11 Player Channel
    /// </summary>
    public Dictionary<int, string> SoundList => soundList;

    void Start()
    {
        Init();
    }

    
    void Update()
    {
        
    }


    public AudioClip GetOrAddAudioClip(string _path)
    {
        AudioClip audioClip = null;

        if (_path.Contains("Sounds/") == false)
            _path = $"Sounds/{_path}";


        if (audioDic.TryGetValue(_path, out audioClip) == false)
        {
            audioClip = Resources.Load<AudioClip>(_path);
            audioDic.Add(_path, audioClip);
        }


        if (audioClip == null)
            Debug.Log($"AudioClip Missing ! {_path}");

        return audioClip;
    }

    public void PlayAudio(string _path, AudioSource _sorce, ESoundType _type ,float _pitch = 1f)
    {
        AudioClip _clip = GetOrAddAudioClip(_path);
        if (_clip == null)
            return;

        if(_type == ESoundType.Effect)
        {
            _sorce.pitch = _pitch;
            _sorce.PlayOneShot(_clip, _pitch);
        }
        else
        {
            if (_sorce.isPlaying)
                _sorce.Stop();

            _sorce.pitch = _pitch;
            _sorce.clip = _clip;
            _sorce.Play();
        }

       
    }
    

    private void Init()
    {
        soundList.Add(0, "InsideRoomWalkSound");
        soundList.Add(1, "InsideRoomRunSound");
        soundList.Add(2, "LongGrassWalkSound");
        soundList.Add(3, "LongGrassRunSound");
        soundList.Add(4, "GrassLandWalkSound");
        soundList.Add(5, "GrassLandRunSound");
        soundList.Add(6, "LongGrassWalkSound");
        soundList.Add(7, "LongGrassRunSound");
        soundList.Add(8, "Drink");
        soundList.Add(9, "PutOnItem");
        //----움직임---


        soundList.Add(30, "OpenDoorSound");
        soundList.Add(31, "CloseDoorSound");
        soundList.Add(32, "OpenTheClosetSound");
        soundList.Add(33, "CloseTheClosetSound");
        soundList.Add(34, "ButtonPressSound");
        soundList.Add(35, "ElevatorSound");

        //----효과음엘베문----



        soundList.Add(51, "GlassSound");
        soundList.Add(52, "LightBlink");
        soundList.Add(53, "AnalogSound");

        //---아이템 관련----

    }
}

