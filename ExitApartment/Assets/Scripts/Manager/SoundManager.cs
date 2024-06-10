using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private Dictionary<string, AudioClip> audioDic = new Dictionary<string, AudioClip>();
    public Dictionary<string, AudioClip> AudioDic => audioDic;

    void Start()
    {
        
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
    
}

